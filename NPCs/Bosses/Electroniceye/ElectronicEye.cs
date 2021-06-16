using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Termination.Projectiles;
using Termination;
using System.Collections.Generic;
using System;

namespace Termination.NPCs.Bosses.ElectronicEye
{
    [AutoloadBossHead]
    public class ElectronicEye : ModNPC
    {
        private float maxSpeed = 5f;
        private bool initCheck1 = false;
        private bool initCheck2 = false;
        private bool phase2Check1 = false;
        private bool phase2Check2 = false;
        private bool phase3Check1 = false;
        private bool phase3Check2 = false;

        private float timer1;
        private float timer2;
        private float breakDownTimer = -1f;

        private float RotationMultiplier = 1f;

        public bool Boolean1;
        private float angle;
        private float randomTimer;

        public List<Vector2> BallMetalCenters { get; private set; } = new List<Vector2>();

        private List<float> rotationSpeed = new List<float>(); //0.025f
        private List<float> rotation = new List<float>();
        private List<float> distance = new List<float>();//300
        private List<float> targetDistance = new List<float>();
        private List<bool> isRising = new List<bool>();

        public List<BallMetal> BallMetals = new List<BallMetal>(4);
        public float ElectronicEyeDistributePhase { get; private set; }
        public bool MaceLaunchCheck { get; private set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electronic Orb");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 100000;
            npc.damage = 50;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 128;
            npc.alpha = 1;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 12f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            music = MusicID.Boss3;
            bossBag = mod.ItemType("ElectronicEyeBag");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (npc.lifeMax + 50000);
            npc.damage = (int)(npc.damage * 0.7f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public bool Unused
        {
            get
            {
                return npc.ai[0] != 0f;
            }
            set
            {
                npc.ai[0] = value ? 1f : 0f;
            }
        }

        public float Phase
        {
            get
            {
                return npc.ai[1];
            }
            set
            {
                npc.ai[1] = value;
            }
        }

        public float AttackID
        {
            get
            {
                return npc.ai[2];
            }
            set
            {
                npc.ai[2] = value;
            }
        }

        public float AttackTimer
        {
            get
            {
                return npc.ai[3];
            }
            set
            {
                npc.ai[3] = value;
            }
        }

        protected NPC ElectronicEyeDrone
        {
            get
            {
                return Main.npc[(int)npc.ai[0]];
            }
        }

        public override void AI()
        {
            Initialize();

            if (Main.expertMode == true)
            {
                if (npc.life >= (npc.lifeMax * 2 / 3))
                    Phase = 1f;
                else if (npc.life <= (npc.lifeMax * 2 / 3) && npc.life >= (npc.lifeMax * 1 / 3))
                    Phase = 2f;
                else if (npc.life <= (npc.lifeMax * 1 / 3))
                    Phase = 3f;
            }
            else
            {
                if (npc.life >= (npc.lifeMax * 0.5))
                    Phase = 1f;
                else if (npc.life <= (npc.lifeMax * 0.5))
                    Phase = 2f;
            }

            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                // Filters.Scene.Activate("ElectronicEyeEffect");

                // Updating a filter
                // Filters.Scene["ElectronicEyeEffect"].GetShader().UseProgress(progress);

                // Filters.Scene["ElectronicEyeEffect"].Deactivate();
            }

            CheckDrones();

            if (Phase == 1f)
                PhaseOneAttacks();
            else if (Phase == 2f)
                PhaseTwoAttacks();
            else if (Phase == 3f)
                PhaseThreeAttacks();

            UpdatePeripheralPositions();


            if (!npc.HasValidTarget)
            {
                npc.TargetClosest(false);

            }
            if (!npc.HasValidTarget)
            {
                npc.velocity = new Vector2(0f, maxSpeed);
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                npc.netUpdate = true;
                return;
            }

            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, 10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        private void PhaseOneAttacks()
        {
            AttackTimer += 2;
            switch (AttackID)
            {
                case 0f:
                    IdleBehavior();
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 1f:
                    AttackTimer -= 1;
                    TeleportAttack();
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 2f:
                    SpawnOrbs1("ElectronicEyeThornBall");
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 3f:
                    MaceAttack();
                    ElectronicEyeDistributePhase = 2;
                    break;
                case 4f:
                    SpawnOrbs2("ElectronicEyeOrb");
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 5f:
                    LaserAttack();
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 6f:
                    WildSpinAttack();
                    ElectronicEyeDistributePhase = 1;
                    break;
            }

            if (AttackTimer >= 720)
                ChooseAttackSequence();
        }
        private void PhaseTwoAttacks()
        {
            Initphase2();

            AttackTimer += 2;
            switch (AttackID)
            {
                case 0f:
                    WildSpinAttack();
                    ElectronicEyeDistributePhase = 1;
                    break;
                case 1f:
                    TeleportAttack();
                    AttackTimer -= 1;
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 2f:
                    SpawnOrbs3("ElectronicEyeThornBall");
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 3f:
                    MaceAttack();
                    ElectronicEyeDistributePhase = 2;
                    break;
                case 4f:
                    SpawnOrbs2("ElectronicEyeOrb");
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 5f:
                    LaserAttack();
                    ElectronicEyeDistributePhase = 0;
                    break;
                case 6f:
                    WildSpinAttack();
                    ElectronicEyeDistributePhase = 1;
                    break;
            }

            if (AttackTimer >= 720)
                ChooseAttackSemiRandom(1, 6);
        }
        private void PhaseThreeAttacks()
        {
            Initphase3();
            Phase3Cinematics();
            switch (AttackID)
            {
                case 0f:
                    RandomMovement();
                    break;
                case 1f:
                    SpawnOrbs4("ElectronicEyeThornBallAngry");
                    break;
                case 2f:
                    CrossLaunch();
                    break;
                case 3f:
                    BounceYeet();
                    break;
                case 4f:
                    RoofLazer();
                    break;
                case 5f:
                    RandomMovement();
                    break;
                case 6f:
                    RandomMovement();
                    break;
            }

            AttackTimer += 1;
            ElectronicEyeDistributePhase = 3;

            if (AttackTimer >= 1000)
                ChooseAttackRandom(5, 5);
        }

        private void ChooseAttackSequence()
        {
            AttackID += 1f;
            if (AttackID >= 7f)
            {
                AttackID = 1f;
            }
            AttackTimer = 0f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
            Boolean1 = false;
        }

        private void ChooseAttackSemiRandom(int min, int max)
        {
            AttackID += 1f;
            if (AttackID >= 7f)
            {
                AttackID = (float)Main.rand.Next(min, max + 1);
            }
            AttackTimer = 0f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
            Boolean1 = false;
        }

        private void ChooseAttackRandom(int min, int max)
        {
            AttackID = Main.rand.Next(min, max + 1);
            AttackTimer = -1f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
            Boolean1 = false;
        }

        private void CheckDrones()
        {
            if (ElectronicEyeDistributePhase != 3)
            {
                if (NPC.AnyNPCs(mod.NPCType("ElectronicEyeDrone")) == true || NPC.AnyNPCs(mod.NPCType("ElectronicEyeAnchor")) == true)
                    npc.dontTakeDamage = true;
                else
                    npc.dontTakeDamage = false;
            }
            else
            {
                if (NPC.AnyNPCs(mod.NPCType("BallMetal")) == true)
                    npc.dontTakeDamage = true;
                else
                    npc.dontTakeDamage = false;
            }

            if (Phase != 3)
            {
                if (!CheckBallLoaded(0))
                    RespawnBall(0);
                if (!CheckBallLoaded(2))
                    RespawnBall(2);
            }
        }
        public void RespawnBall(int i)
        {
            int j = NPC.NewNPC(0, 0, mod.NPCType("BallMetal"), 0, npc.whoAmI, i);
            npc.netUpdate = true;
            Main.npc[j].netUpdate = true;
        }
        public bool CheckBallLoaded(int i)
        {
            if (BallMetals[i] == null)
                return false;
            else if (!BallMetals[i].CheckWorking())
            {
                BallMetals[i] = null;
                return false;
            }
            return true;
        }

        private void Initialize()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
            {
                Phase = 1f;
                AttackID = 0f;
                npc.netUpdate = true;

                for (int i = -1; i < 3; i++)
                {
                    BallMetalCenters.Add(Vector2.Zero);
                    distance.Add(300);
                    targetDistance.Add(20);
                    rotationSpeed.Add(0.025f);
                    isRising.Add(false);
                    rotation.Add(i * MathHelper.PiOver2);

                    BallMetals.Add(null);
                }

                initCheck1 = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                Main.NewText("The Electronic Eye has awoken!", 158, 115, 115);

                initCheck2 = true;
            }
            if (initCheck1 == true && initCheck2 == true && npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
            }
        }

        private void Initphase2()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 1f)
            {
                AttackID = 0f;
                AttackTimer = 0;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                int Drone1 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 400, mod.NPCType("ElectronicEyeDrone"), 0, npc.whoAmI, -1f, 0f, -30f);
                npc.netUpdate = true;
                Main.npc[Drone1].netUpdate = true;

                phase2Check1 = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 1f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                Main.NewText("SIGNIFICANT DAMAGE SUSTAINED: SETTING BOOLEAN VARIABLE <LimitReactor> TO FALSE", 87, 49, 49);
                Main.NewText("DESTROY!!!", 87, 49, 49);

                phase2Check2 = true;
            }
            if (phase2Check1 == true && phase2Check2 == true && npc.localAI[0] == 1f)
            {
                npc.localAI[0] = 2f;
            }
        }

        private void Initphase3()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 2f)
            {
                AttackID = 0f;
                AttackTimer = 0;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;

                RespawnBall(1);
                RespawnBall(3);

                ElectronicEyeDistributePhase = 3;

                phase3Check1 = true;
                npc.netUpdate = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 2f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);

                Main.NewText("MULTIPLE SYSTEM FAILURES DETECTED", 54, 0, 9);

                breakDownTimer = 120f;

                phase3Check2 = true;
            }
            if (phase3Check1 == true && phase3Check2 == true && npc.localAI[0] == 2f)
            {
                npc.localAI[0] = 3f;
            }
        }

        private void Phase3Cinematics()
        {
            switch (breakDownTimer)
            {
                case 180:
                    Main.NewText("UNUSUAL VOLTAGES DETECTED", 54, 0, 9);
                    break;

                case 160:
                    Main.NewText("POWER LEVELS UNSTABLE", 54, 0, 9);
                    break;

                case 140:
                    Main.NewText("SYSTEM CLOCK MISTIME DETECTED", 54, 0, 9);
                    break;

                case 120:
                    Main.NewText("UNAUTHORISED OVERCLOCKING", 54, 0, 9);
                    break;

                case 100:
                    Main.NewText("MULTIPLE MEMORY ERRORS DETECTED", 54, 0, 9);
                    break;

                case 50:
                    Main.NewText("EMERGENCY PROCEDURE; ENABLING CLOSED SHELL - RETRACT AND PROTECT", 54, 0, 9);
                    break;

                case 0:
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    Main.NewText("O V E R   L O A D I N G", 255, 0, 0);
                    break;
            }
            breakDownTimer--;
        }

        public override bool CheckDead()
        {
            Main.PlaySound(SoundID.NPCDeath22, (int)npc.position.X, (int)npc.position.Y);
            Main.NewText("P R I M A R Y   S Y S T E M   F A I L U R E", 128, 0, 0);
            Main.NewText("N A N I T E   C L U S T E R   D I S P E R S I N G", 128, 0, 0);
            SpawnDeathParticles();
            return true;
        }

        //nanite cluster dispersing

        public void SpawnDeathParticles()
        {
            int MaxCounter = Main.rand.Next(100, 150);
            for (int i = 0; i < MaxCounter; i++)
            {
                Vector2 dustLocation = new Vector2(npc.position.X + Main.rand.NextFloat(-100, 100), npc.position.Y + Main.rand.NextFloat(-100, 100));
                Dust.NewDust(dustLocation, npc.width, npc.height, mod.DustType("MotherSpark"), 10f + Main.rand.NextFloat(1f, 5f), -10f - Main.rand.NextFloat(5f, 10f), 1, Color.Black, 2f);
            }
        }
        private void RandomMovement()
        {
            maxSpeed = 10f;
            if (AttackTimer == 0)
            {
                angle = Main.rand.Next(0, 360);
                randomTimer = Main.rand.Next(20, 60);
            }

            if (AttackTimer >= 0 && AttackTimer <= 20)
            {
                TeleportDust(npc.position);
                angle += 2f;
                TeleportDust(TerminationUtils.RegularToComponantVector(Main.player[npc.target].Center, angle, 300, -npc.frame.Size() / 2));
            }
            else if (AttackTimer >= 20 && AttackTimer <= 60 + randomTimer)
            {
                npc.velocity = Vector2.Zero;
                TeleportGroup(TerminationUtils.RegularToComponantVector(Main.player[npc.target].Center, angle, 300, -npc.frame.Size() / 2));
                Lazer("ElectronicEyeThornBallAngry", Main.player[npc.target].Center, 10f, 0, 0f);
                angle += 2f;
            }
            else if (AttackTimer >= 60 + randomTimer && AttackTimer <= 100 + randomTimer)
            {
                npc.velocity = new Vector2((float)Math.Cos(MathHelper.ToRadians(angle + 90)), (float)Math.Sin(MathHelper.ToRadians(angle + 90)));
                npc.velocity.Normalize();
                npc.velocity *= maxSpeed;
                npc.velocity += Main.player[npc.target].velocity;

            }
            else if (AttackTimer >= 80 + randomTimer)
            {
                ChooseAttackRandom(2, 5);
            }
        }

        private void RoofLazer()
        {
            maxSpeed = 10f;
            if (AttackTimer == 0)
            {
                randomTimer = 500;
            }

            if (AttackTimer >= 0 && AttackTimer <= 20)
            {
                TeleportDust(npc.position);
                TeleportDust(new Vector2(Main.player[npc.target].Center.X + randomTimer, Main.player[npc.target].Center.Y - 300));
                randomTimer -= 10;
            }
            else if (AttackTimer >= 20 && AttackTimer <= 100)
            {
                npc.velocity = Vector2.Zero;
                TeleportGroup(new Vector2(Main.player[npc.target].Center.X + randomTimer, Main.player[npc.target].Center.Y - 300));
                randomTimer -= 20;
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
            }
            else if (AttackTimer >= 100 && AttackTimer <= 120)
            {
                npc.velocity = new Vector2(-5f, 0);
                npc.velocity.Normalize();
                npc.velocity *= maxSpeed;
                npc.velocity += Main.player[npc.target].velocity;
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
            }
            else if (AttackTimer >= 120)
            {
                ChooseAttackRandom(2, 5);
            }
        }

        public void BounceYeet()
        {
            maxSpeed = 10f;

            if (AttackTimer >= 0 && AttackTimer <= 20)
            {
                TeleportDust(npc.position);
                TeleportDust(new Vector2(Main.player[npc.target].Center.X - (npc.width / 2), Main.player[npc.target].Center.Y - 300));
            }
            else if (AttackTimer >= 20 && AttackTimer <= 100)
            {
                npc.velocity = Vector2.Zero;
                TeleportGroup(new Vector2(Main.player[npc.target].Center.X - (npc.width / 2), Main.player[npc.target].Center.Y - 300));
                Lazer("ElectronicEyeThornBallAngry", npc.Center + new Vector2(0, 100), 20f, 50f, 0f);
            }
            else if (AttackTimer >= 100 && AttackTimer <= 120)
            {
                npc.velocity = Vector2.Zero;
                npc.velocity += Main.player[npc.target].velocity;
                Lazer("ElectronicEyeThornBallAngry", npc.Center + new Vector2(0, 100), 20f, 50f, 0f);
            }
            else if (AttackTimer >= 120)
            {
                ChooseAttackRandom(2, 5);
            }
        }

        private void CrossLaunch()
        {
            maxSpeed = 10f;
            if (AttackTimer == 0)
            {
                npc.velocity = Vector2.Zero;
                angle = Main.rand.Next(0, 360);
                if (Main.rand.NextBool())
                    randomTimer = 1;
                else
                    randomTimer = -1;
            }

            if (AttackTimer >= 0 && AttackTimer <= 180)
            {
                angle += randomTimer;
                Vector2 vector = TerminationUtils.RegularToComponantVector(Main.player[npc.target].Center, angle, 500, -npc.frame.Size() / 2);
                Lazer("ElectronicEyeThornBallFollow", Main.player[npc.target].Center, 10f, 0, 0f, vector);
            }
            else if (AttackTimer >= 200)
            {
                ChooseAttackRandom(2, 5);
            }
        }

        private void SpiralYeet()
        {
            maxSpeed = 10f;

            if (AttackTimer == 0)
            {
                npc.velocity = Vector2.Zero;
                angle = Main.rand.Next(0, 360);
            }

            if (AttackTimer >= 0 && AttackTimer <= 360)
            {
                angle += 1f;
                Vector2 vector = TerminationUtils.RegularToComponantVector(npc.Center, angle, 500, -npc.frame.Size() / 2);
                Lazer("ElectronicEyeThornBallFollow", vector, 10f, 0, 28f);
            }
            else if (AttackTimer >= 380)
            {
                ChooseAttackRandom(2, 5);
            }
        }
        private void MaceAttack()
        {
            if (!Boolean1)
            {
                Boolean1 = true;
                RotationMultiplier *= -1f;
            }
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
        }

        private void IdleBehavior()
        {
            maxSpeed = 5f;

            Vector2 offset = npc.Center - Main.player[npc.target].Center;
            offset *= 0.9f;
            Vector2 target = offset.RotatedBy(Main.expertMode ? 0.03f : 0.02f);
            CapVelocity(ref target, 240f);
            Vector2 change = target - offset;
            CapVelocity(ref change, maxSpeed);
            ModifyVelocity(change);
            CapVelocity(ref npc.velocity, maxSpeed);
        }

        private void TeleportAttack()
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;

            if (AttackTimer <= 179)
            {
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 179 && AttackTimer <= 180)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }

            if (AttackTimer >= 180 && AttackTimer <= 314)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 314 && AttackTimer <= 315)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2)));
            }

            if (AttackTimer >= 315 && AttackTimer <= 449)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2)));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 449 && AttackTimer <= 450)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2)));
            }

            if (AttackTimer >= 450 && AttackTimer <= 584)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2)));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 584 && AttackTimer <= 585)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2)));
            }

            if (AttackTimer >= 585 && AttackTimer <= 719)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2)));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 717 && AttackTimer <= 728)
            {
                TeleportGroup(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }
        }

        private void TeleportDust(Vector2 dustlocation)
        {
            Dust.NewDust(dustlocation, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
            List<Vector2> vectors = new List<Vector2>();
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                    vectors.Add(new Vector2(dustlocation.X + (float)(i * Main.rand.Next(1, 50)), dustlocation.Y + (float)(j * Main.rand.Next(1, 50))));
            }

            foreach (Vector2 v in vectors)
                Dust.NewDust(v, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
        }

        private void LaserAttack()
        {
            Lazer("ElectronicEyeBeam", Main.player[npc.target].Center, 10f);
            IdleBehavior();
        }

        private void DeathBeam()
        {
            if (timer1 <= 999)
            {
                int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("ElectronicEyePrismGun"), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;
                Main.projectile[proj1].scale = 1f;

                timer1 = 1000;
            }
        }

        private void SpawnOrbs1(string whattoshoot)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            if (timer1 == 30)
            {
                for (int i = 0; i <= 3; i++)
                {
                    int j = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                    Main.projectile[j].friendly = false;
                    Main.projectile[j].hostile = true;
                    Main.projectile[j].scale = 1f;
                }
            }
            else if (timer1 == 90)
            {
                for (int i = 0; i <= 6; i++)
                {
                    int j;
                    if (i % 2 == 0)
                        j = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                    else
                        j = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                    Main.projectile[j].friendly = false;
                    Main.projectile[j].hostile = true;
                    Main.projectile[j].scale = 1f;
                }
            }
            else if (timer1 == 150)
            {
                for (int i = 0; i <= 6; i++)
                {
                    int j;
                    if (i % 2 == 0)
                        j = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                    else
                        j = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                    Main.projectile[j].friendly = false;
                    Main.projectile[j].hostile = true;
                    Main.projectile[j].scale = 1f;
                }
            }
            else if (timer1 == 360)
            {
                timer1 = 0;
            }

            timer1 += 1;
        }

        private void SpawnOrbs2(string whattoshoot)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            if (timer1 == 30)
            {
                int i = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[i].friendly = false;
                Main.projectile[i].hostile = true;
                Main.projectile[i].scale = 1f;
            }
            else if (timer1 == 90)
            {
                int i = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[i].friendly = false;
                Main.projectile[i].hostile = true;
                Main.projectile[i].scale = 1f;
                int j = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[j].friendly = false;
                Main.projectile[j].hostile = true;
                Main.projectile[j].scale = 1f;
            }
            else if (timer1 == 150)
            {
                int i = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[i].friendly = false;
                Main.projectile[i].hostile = true;
                Main.projectile[i].scale = 1f;
                int j = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[j].friendly = false;
                Main.projectile[j].hostile = true;
                Main.projectile[j].scale = 1f;
            }
            else if (timer1 == 360)
            {
                timer1 = 0;
            }

            timer1 += 1;
        }

        private void SpawnOrbs3(string whattoshoot)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            if (timer1 % 10 == 0 && timer1 <= 200)
            {
                int proj1 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-500, 500), npc.Center.Y + Main.rand.Next(-500, 500), 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;
                Main.projectile[proj1].scale = 1f;
            }
            else if (timer1 == 360)
            {
                timer1 = 0;
            }

            timer1 += 1;
        }
        private void SpawnOrbs4(string whattoshoot)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            if (timer1 % 10 == 0 && timer1 <= 300)
            {
                int proj1 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-500, 500), npc.Center.Y + Main.rand.Next(-500, 500), 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;
                Main.projectile[proj1].scale = 1f;
            }
            else if (timer1 == 360)
            {
                timer1 = 0;
            }

            timer1 += 1;
        }

        private void WildSpinAttack()
        {
            maxSpeed = 15f;

            Vector2 offset = npc.Center - Main.player[npc.target].Center;
            offset *= 0.9f;
            Vector2 target = offset.RotatedBy(Main.expertMode ? 0.03f : 0.02f);
            CapVelocity(ref target, 300f);
            Vector2 change = target - offset;
            CapVelocity(ref change, maxSpeed);
            ModifyVelocity(change);
            CapVelocity(ref npc.velocity, maxSpeed);
        }

        private void Lazer(string whattoshoot, Vector2 wheretoshootit, float framedelay)
        {
            if (timer2 >= framedelay)
            {
                float inaccuracy = 3f * (npc.life / npc.lifeMax);
                Vector2 shootVel = wheretoshootit - npc.Center + new Vector2(Main.rand.NextFloat(-inaccuracy, inaccuracy), Main.rand.NextFloat(-inaccuracy, inaccuracy));
                shootVel.Normalize();
                shootVel *= 28f;
                int k = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[k].friendly = false;
                Main.projectile[k].hostile = true;
                Main.projectile[k].scale = 1f;
                timer2 = 0;
            }
            else
            {
                timer2 += 1;
            }
        }

        private void Lazer(string whatToShoot, Vector2 whereToShootIt, float frameDelay, float angleVariation, float shootSpeed)
        {
            if (timer2 >= frameDelay)
            {
                Vector2 shootVel = whereToShootIt - npc.Center + new Vector2(Main.rand.NextFloat(-angleVariation, angleVariation), Main.rand.NextFloat(-angleVariation, angleVariation));
                shootVel.Normalize();
                shootVel *= shootSpeed;
                int k = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType(whatToShoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[k].friendly = false;
                Main.projectile[k].hostile = true;
                Main.projectile[k].scale = 1f;
                timer2 = 0;
            }
            else
            {
                timer2 += 1;
            }
        }

        private void Lazer(string whatToShoot, Vector2 whereToShootIt, float frameDelay, float angleVariation, float shootSpeed, Vector2 vector)
        {
            if (timer2 >= frameDelay)
            {
                Vector2 shootVel = whereToShootIt - npc.Center + new Vector2(Main.rand.NextFloat(-angleVariation, angleVariation), Main.rand.NextFloat(-angleVariation, angleVariation));
                shootVel.Normalize();
                shootVel *= shootSpeed;
                int k = Projectile.NewProjectile(vector.X, vector.Y, shootVel.X, shootVel.Y, mod.ProjectileType(whatToShoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[k].friendly = false;
                Main.projectile[k].hostile = true;
                Main.projectile[k].scale = 1f;
                timer2 = 0;
            }
            else
            {
                timer2 += 1;
            }
        }

        private void ModifyVelocity(Vector2 modify, float weight = 0.05f)
        {
            npc.velocity = Vector2.Lerp(npc.velocity, modify, weight);
        }

        private void CapVelocity(ref Vector2 velocity, float max)
        {
            if (velocity.Length() > max)
            {
                velocity.Normalize();
                velocity *= max;
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PhantomTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PhantomMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SusPlating"), Main.rand.Next(5, 15));
                int reward = 0;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        reward = mod.ItemType("DamagedControlCircuit");
                        break;

                    case 1:
                        reward = mod.ItemType("SpectreGun");
                        break;

                    case 2:
                        reward = mod.ItemType("PhantomSphere");
                        break;

                    case 3:
                        reward = mod.ItemType("PaladinStaff");
                        break;
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, reward);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        private const int Frame_EyeGlow1 = 0;
        private const int Frame_EyeGlow2 = 1;
        private const int Frame_EyeGlow3 = 2;
        private const int Frame_EyeGlow4 = 3;
        private int FrameCycle = 0;

        public override void FindFrame(int frameHeight)
        {
            // For the most part, our animation matches up with our states.
            FrameCycle = 1;

            if (FrameCycle == 1)
            {
                // Here we have 4 frames that we want to cycle through.
                npc.frameCounter++;
                if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_EyeGlow1 * frameHeight;
                }
                else if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_EyeGlow2 * frameHeight;
                }
                else if (npc.frameCounter < 90)
                {
                    npc.frame.Y = Frame_EyeGlow3 * frameHeight;
                }
                else if (npc.frameCounter < 120)
                {
                    npc.frame.Y = Frame_EyeGlow4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                 Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 1f);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 1f);
        }
        public void UpdatePeripheralPositions()
        {
            switch (ElectronicEyeDistributePhase)
            {

                case 0:
                    for (int i = 0; i < rotationSpeed.Count; i++)
                        rotationSpeed[i] = 0.025f;
                    for (int i = 0; i < distance.Count; i++)
                        if (distance[i] >= 200)
                            distance[i]--;
                    break;


                case 1:
                    for (int i = 0; i < rotationSpeed.Count; i++)
                        rotationSpeed[i] = 0.1f;
                    for (int i = 0; i < distance.Count; i++)
                        if (distance[i] <= 400)
                            distance[i]++;
                    break;

                case 3:
                    for (int i = 0; i < rotationSpeed.Count; i++)
                        rotationSpeed[i] = 0.1f;
                    for (int i = 0; i < distance.Count; i++)
                    {
                        if (isRising[i])
                        {
                            if (targetDistance[i] - distance[i] < 0)
                            {
                                isRising[i] = false;
                                targetDistance[i] = Main.rand.Next(30, 50);
                            }
                            distance[i] += 5;
                        }
                        else
                        {
                            if (targetDistance[i] - distance[i] > 0)
                            {
                                isRising[i] = true;
                                targetDistance[i] = Main.rand.Next(50, 70);
                            }
                            distance[i] -= 5;
                        }
                    }
                    break;

            }

            if (ElectronicEyeDistributePhase <= 1 || ElectronicEyeDistributePhase == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    rotation[i] += rotationSpeed[i] * RotationMultiplier;
                    BallMetalCenters[i] = TerminationUtils.PolarPos(npc.Center, distance[i], rotation[i]);
                }
            }
            else if (ElectronicEyeDistributePhase == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 TangentalVector = new Vector2((float)Math.Cos(rotation[i] + (MathHelper.PiOver2 * RotationMultiplier)), (float)Math.Sin(rotation[i] + (MathHelper.PiOver2 * RotationMultiplier))) * 5f;
                    if (AttackTimer <= 360)
                        BallMetalCenters[i] += TangentalVector;
                    if (AttackTimer >= 360)
                        BallMetalCenters[i] -= TangentalVector;
                }
            }
        }

        public void MovePeripheralsToRelative()
        {
            if (ElectronicEyeDistributePhase <= 1 || ElectronicEyeDistributePhase == 3)
                for (int i = 0; i < 4; i++)
                    BallMetalCenters[i] = TerminationUtils.PolarPos(npc.Center, distance[i], rotation[i]);
        }
        public void TeleportGroup(Vector2 position)
        {
            npc.position = position;
            MovePeripheralsToRelative();
        }
    }
}
