using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Termination.Projectiles;
using Termination;

namespace Termination.NPCs.Bosses.ElectronicEye
{
    [AutoloadBossHead]
    public class ElectronicEye : ModNPC
    {
        private float maxSpeed = 5f;
        private float SummonTimer = 0f;
        private bool initcheck1 = false;
        private bool initcheck2 = false;
        private bool phase2check1 = false;
        private bool phase2check2 = false;
        private bool phase3check1 = false;
        private bool phase3check2 = false;

        private float timer1 = 0f;
        private float timer2 = 0f;
        private Vector2 dustlocation1;
        private Vector2 dustlocation2;
        private Vector2 dustlocation3;
        private Vector2 dustlocation4;

        public static Vector2 ballmetalcenter1;
        public static Vector2 ballmetalcenter2;
        public static Vector2 ballmetalcenter3;
        public static Vector2 ballmetalcenter4;

        public static Vector2 ElectronicEyePosition;

        public static Vector2 npctargetlocation;

        private float RotationSpeed = 0.025f;
        private float Rotation;
        private float distance = 300;

        private Vector2 shootvel;

        public static float ElectronicEyeDistributePhase { get; set; }
        public static bool MaceLaunchCheck { get; set; }

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

            if (ElectronicEyeDistributePhase <= 1)
            {
                Rotation += RotationSpeed;
                ballmetalcenter1 = TerminationHelper.PolarPos(npc.Center, distance, MathHelper.ToRadians(Rotation));
                ballmetalcenter2 = TerminationHelper.PolarPos(npc.Center, distance, MathHelper.ToRadians(Rotation + MathHelper.Pi));
                ballmetalcenter3 = TerminationHelper.PolarPos(npc.Center, distance, MathHelper.ToRadians(Rotation + MathHelper.PiOver2));
                ballmetalcenter4 = TerminationHelper.PolarPos(npc.Center, distance, MathHelper.ToRadians(Rotation - MathHelper.PiOver2));
            }

            ElectronicEyePosition = npc.Center;

            if (ElectronicEyeDistributePhase == 1)
            {
                RotationSpeed = 0.1f;
                if (distance <= 400)
                {
                    distance++;
                }
            }
            else
            {
                RotationSpeed = 0.025f;
                if (distance >= 200)
                {
                    distance--;
                }
            }


            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                // Filters.Scene.Activate("ElectronicEyeEffect");

                // Updating a filter
                // Filters.Scene["ElectronicEyeEffect"].GetShader().UseProgress(progress);

                // Filters.Scene["ElectronicEyeEffect"].Deactivate();
            }

            CheckDroneActive();

            if (Main.expertMode == true)
            {
                if (npc.life <= npc.lifeMax && npc.life >= (npc.lifeMax * 2 / 3))
                {
                    Phase = 1f;
                }
                else if (npc.life <= (npc.lifeMax * 2 / 3) && npc.life >= (npc.lifeMax * 1 / 3))
                {
                    Phase = 2f;
                }
                else if (npc.life <= (npc.lifeMax * 1 / 3))
                {
                    Phase = 3f;
                }
            }
            else
            {
                if (npc.life <= npc.lifeMax && npc.life >= (npc.lifeMax * 0.5))
                {
                    Phase = 1f;
                }
                else if (npc.life <= (npc.lifeMax * 0.5) && npc.life >= -100000)
                {
                    Phase = 2f;
                }
            }

            if (Phase == 1f)
            {
                AttackTimer += 2;
                if (AttackID == 0f)
                {
                    IdleBehavior();
                }
                else if (AttackID == 1f)
                {
                    TeleportAttack();
                    AttackTimer -= 1;
                }
                else if (AttackID == 2f)
                {
                    SpawnOrbs1("ElectronicEyeThornBall");
                }
                else if (AttackID == 3f)
                {
                    MaceAttack();
                }
                else if (AttackID == 4f)
                {
                    SpawnOrbs2("ElectronicEyeOrb");
                }
                else if (AttackID == 5f)
                {
                    LaserAttack();
                }
                else if (AttackID == 6f)
                {
                    WildSpinAttack();
                }
                if (AttackTimer >= 720)
                {
                    ChooseAttackSequence();
                }
            }
            else if (Phase == 2f)
            {
                Initphase2();

                AttackTimer += 2;
                if (AttackID == 0f)
                {
                    WildSpinAttack();
                }
                else if (AttackID == 1f)
                {
                    TeleportAttack();
                    AttackTimer -= 1;
                }
                else if (AttackID == 2f)
                {
                    SpawnOrbs3("ElectronicEyeThornBall");
                }
                else if (AttackID == 3f)
                {
                    MaceAttack();
                }
                else if (AttackID == 4f)
                {
                    SpawnOrbs2("ElectronicEyeOrb");
                }
                else if (AttackID == 5f)
                {
                    LaserAttack();
                }
                else if (AttackID == 6f)
                {
                    WildSpinAttack();
                }
                if (AttackTimer >= 720)
                {
                    ChooseAttackSemiRandom();
                }
            }
            else if (Phase == 3f)
            {
                Initphase3();

                AttackTimer += 1;
                if (AttackID == 0f)
                {
                    IdleBehavior();
                }
                else if (AttackID == 1f)
                {
                    SpawnOrbs4("ElectronicEyeThornBallAngry");
                }
                else if (AttackID == 2f)
                {
                    DeathBeam();
                    WildSpinAttack();
                }
                else if (AttackID == 3f)
                {
                    DeathBeam();
                    WildSpinAttack();
                }
                else
                {
                    WildSpinAttack();
                }

                if (AttackTimer >= 720)
                {
                    ChooseAttackRandom();
                }
            }
        }

        private void ChooseAttackSequence()
        {
            AttackID = 1f;
            if (AttackID >= 7f)
            {
                AttackID = 1f;
            }
            AttackTimer = 0f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
        }

        private void ChooseAttackSemiRandom()
        {
            AttackID += 1f;
            if (AttackID >= 7f)
            {
                AttackID = (float)Main.rand.Next(1, 6);
            }
            AttackTimer = 0f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
        }

        private void ChooseAttackRandom()
        {
            AttackID = Main.rand.Next(1, 3);
            AttackTimer = 0f;
            npc.TargetClosest(false);
            npc.netUpdate = true;
        }

        private void CheckDroneActive()
        {
            if (NPC.AnyNPCs(mod.NPCType("ElectronicEyeDrone")) == true)
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            if (NPC.AnyNPCs(mod.NPCType("BallMetal")) == false)
            {
                int spawnX = (int)npc.Center.X;
                int spawnY = (int)npc.Center.Y + 64;
                int left = NPC.NewNPC(spawnX - 128, spawnY, mod.NPCType("BallMetal"), 0, npc.whoAmI, -1f, 0f, -30f);

                Main.npc[left].netUpdate = true;
            }

            if (NPC.AnyNPCs(mod.NPCType("BallMetal2")) == false)
            {
                int spawnX = (int)npc.Center.X;
                int spawnY = (int)npc.Center.Y + 64;
                int right = NPC.NewNPC(spawnX + 128, spawnY, mod.NPCType("BallMetal2"), 0, npc.whoAmI, 1f, 0f, -60f);

                Main.npc[right].netUpdate = true;
            }

            if (Phase == 3f)
            {
                if (NPC.AnyNPCs(mod.NPCType("BallMetal3")) == false)
                {
                    int spawnX = (int)npc.Center.X;
                    int spawnY = (int)npc.Center.Y + 64;
                    int left = NPC.NewNPC(spawnX - 128, spawnY, mod.NPCType("BallMetal3"), 0, npc.whoAmI, -1f, 0f, -30f);

                    Main.npc[left].netUpdate = true;
                }

                if (NPC.AnyNPCs(mod.NPCType("BallMetal4")) == false)
                {
                    int spawnX = (int)npc.Center.X;
                    int spawnY = (int)npc.Center.Y + 64;
                    int right = NPC.NewNPC(spawnX + 128, spawnY, mod.NPCType("BallMetal4"), 0, npc.whoAmI, 1f, 0f, -60f);

                    Main.npc[right].netUpdate = true;
                }
            }
        }

        private void Initialize()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
            {
                Phase = 1f;
                AttackID = 0f;
                int spawnX = (int)npc.Center.X;
                int spawnY = (int)npc.Center.Y + 64;
                int left = NPC.NewNPC(spawnX - 128, spawnY, mod.NPCType("BallMetal"), 0, npc.whoAmI, -1f, 0f, -30f);
                int right = NPC.NewNPC(spawnX + 128, spawnY, mod.NPCType("BallMetal2"), 0, npc.whoAmI, 1f, 0f, -60f);
                npc.netUpdate = true;
                Main.npc[left].netUpdate = true;
                Main.npc[right].netUpdate = true;

                initcheck1 = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                Main.NewText("The Electronic Eye has awoken!", 158, 115, 115);

                initcheck2 = true;
            }
            if (initcheck1 == true && initcheck2 == true && npc.localAI[0] == 0f)
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
                int spawnX = (int)npc.Center.X;
                int spawnY = (int)npc.Center.Y + 400;
                int Drone1 = NPC.NewNPC(spawnX, spawnY, mod.NPCType("ElectronicEyeDrone"), 0, npc.whoAmI, -1f, 0f, -30f);
                npc.netUpdate = true;
                Main.npc[Drone1].netUpdate = true;

                phase2check1 = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 1f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                Main.NewText("DAMAGE SUSTAINED: SETTING BOOLEAN PROTOCAL (HOLDBACK) TO FALSE", 87, 49, 49);
                Main.NewText("DESTROY!!!", 87, 49, 49);

                phase2check2 = true;
            }
            if (phase2check1 == true && phase2check2 == true && npc.localAI[0] == 1f)
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
                int spawnX = (int)npc.Center.X;
                int spawnY = (int)npc.Center.Y + 400;
                int Drone2 = NPC.NewNPC(spawnX + 500, spawnY, mod.NPCType("ElectronicEyeDrone"), 0, npc.whoAmI, -1f, 0f, -30f);
                int Drone3 = NPC.NewNPC(spawnX - 500, spawnY, mod.NPCType("ElectronicEyeDrone"), 0, npc.whoAmI, -1f, 0f, -30f);
                npc.netUpdate = true;
                Main.npc[Drone2].netUpdate = true;
                Main.npc[Drone3].netUpdate = true;

                phase3check1 = true;
            }
            if (Main.netMode != NetmodeID.Server && npc.localAI[0] == 2f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);

                Main.NewText("MULTIPLE SYSTEM FAILURES DETECTED", 54, 0, 9);
                Main.NewText("UNUSUAL VOLTAGES DETECTED", 54, 0, 9);
                Main.NewText("CLOCK LEVELS IN OVERDRIVE", 54, 0, 9);
                Main.NewText("POWER UNSTABLE", 54, 0, 9);
                Main.NewText("UNAUTHORISED OVERCLOCKING", 54, 0, 9);
                Main.NewText("TIME TO FRY", 54, 0, 9);

                phase3check2 = true;
            }
            if (phase3check1 == true && phase3check2 == true && npc.localAI[0] == 2f)
            {
                npc.localAI[0] = 3f;
            }
        }

        private void MaceAttack()
        {
            ElectronicEyeDistributePhase = 2;

            npc.velocity.X = 0;
            npc.velocity.Y = 0;
        }

        private void IdleBehavior()
        {
            ElectronicEyeDistributePhase = 0;
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

            ElectronicEyeDistributePhase = 0;

            if (AttackTimer <= 179)
            {
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 179 && AttackTimer <= 180)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2));
            }

            if (AttackTimer >= 180 && AttackTimer <= 314)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 314 && AttackTimer <= 315)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2));
            }

            if (AttackTimer >= 315 && AttackTimer <= 449)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(300, 0) - (npc.frame.Size() / 2));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 449 && AttackTimer <= 450)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2));
            }

            if (AttackTimer >= 450 && AttackTimer <= 584)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(0, -300) - (npc.frame.Size() / 2));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 584 && AttackTimer <= 585)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2));
            }

            if (AttackTimer >= 585 && AttackTimer <= 719)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(-300, 0) - (npc.frame.Size() / 2));
                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
                TeleportDust(Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2)));
            }
            else if (AttackTimer >= 717 && AttackTimer <= 728)
            {
                npc.position = Main.player[npc.target].Center + (new Vector2(0, 300) - (npc.frame.Size() / 2));
            }
        }

        public virtual void TeleportDust(Vector2 dustlocation)
        {
            Dust.NewDust(dustlocation, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);

            dustlocation1.X = dustlocation.X + Main.rand.Next(1, 50);
            dustlocation1.Y = dustlocation.Y + Main.rand.Next(1, 50);
            dustlocation2.X = dustlocation.X - Main.rand.Next(1, 50);
            dustlocation2.Y = dustlocation.Y - Main.rand.Next(1, 50);
            dustlocation3.X = dustlocation.X + Main.rand.Next(1, 50);
            dustlocation3.Y = dustlocation.Y - Main.rand.Next(1, 50);
            dustlocation4.X = dustlocation.X - Main.rand.Next(1, 50);
            dustlocation4.Y = dustlocation.Y + Main.rand.Next(1, 50);

            Dust.NewDust(dustlocation1, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
            Dust.NewDust(dustlocation2, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
            Dust.NewDust(dustlocation3, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
            Dust.NewDust(dustlocation4, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
        }

        private void LaserAttack()
        {
            Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
            IdleBehavior();
        }

        private void DeathBeam()
        {
            if (timer1 <= 999)
            {
                int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("ElectronicEyePrismGun"), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;

                timer1 = 1000;
            }

        }

        private void SpawnOrbs1(string whattoshoot)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            if (timer1 == 30)
            {
                int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;
                Main.projectile[proj1].scale = 1f;

                int proj2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj2].friendly = false;
                Main.projectile[proj2].hostile = true;
                Main.projectile[proj2].scale = 1f;

                int proj3 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj3].friendly = false;
                Main.projectile[proj3].hostile = true;
                Main.projectile[proj3].scale = 1f;
            }
            else if (timer1 == 90)
            {
                int proj4 = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj4].friendly = false;
                Main.projectile[proj4].hostile = true;
                Main.projectile[proj4].scale = 1f;
                int proj5 = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj5].friendly = false;
                Main.projectile[proj5].hostile = true;
                Main.projectile[proj5].scale = 1f;

                int proj6 = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj6].friendly = false;
                Main.projectile[proj6].hostile = true;
                Main.projectile[proj6].scale = 1f;
                int proj7 = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj7].friendly = false;
                Main.projectile[proj7].hostile = true;
                Main.projectile[proj7].scale = 1f;

                int proj8 = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj8].friendly = false;
                Main.projectile[proj8].hostile = true;
                Main.projectile[proj8].scale = 1f;
                int proj9 = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj9].friendly = false;
                Main.projectile[proj9].hostile = true;
                Main.projectile[proj9].scale = 1f;
            }
            else if (timer1 == 150)
            {
                int proj10 = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj10].friendly = false;
                Main.projectile[proj10].hostile = true;
                Main.projectile[proj10].scale = 1f;
                int proj11 = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj11].friendly = false;
                Main.projectile[proj11].hostile = true;
                Main.projectile[proj11].scale = 1f;

                int proj12 = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj12].friendly = false;
                Main.projectile[proj12].hostile = true;
                Main.projectile[proj12].scale = 1f;
                int proj13 = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj13].friendly = false;
                Main.projectile[proj13].hostile = true;
                Main.projectile[proj13].scale = 1f;

                int proj14 = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj14].friendly = false;
                Main.projectile[proj14].hostile = true;
                Main.projectile[proj14].scale = 1f;
                int proj15 = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj15].friendly = false;
                Main.projectile[proj15].hostile = true;
                Main.projectile[proj15].scale = 1f;
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
                int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 400, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj1].friendly = false;
                Main.projectile[proj1].hostile = true;
                Main.projectile[proj1].scale = 1f;
            }
            else if (timer1 == 90)
            {
                int proj4 = Projectile.NewProjectile(npc.Center.X + 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj4].friendly = false;
                Main.projectile[proj4].hostile = true;
                Main.projectile[proj4].scale = 1f;
                int proj5 = Projectile.NewProjectile(npc.Center.X - 100, npc.Center.Y - 350, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj5].friendly = false;
                Main.projectile[proj5].hostile = true;
                Main.projectile[proj5].scale = 1f;
            }
            else if (timer1 == 150)
            {
                int proj10 = Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj10].friendly = false;
                Main.projectile[proj10].hostile = true;
                Main.projectile[proj10].scale = 1f;
                int proj11 = Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y - 300, 0, 0, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[proj11].friendly = false;
                Main.projectile[proj11].hostile = true;
                Main.projectile[proj11].scale = 1f;
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
            if (timer1 == 30 || timer1 == 50 || timer1 == 70 || timer1 == 90 || timer1 == 100 || timer1 == 110 || timer1 == 120 || timer1 == 130 || timer1 == 140 || timer1 == 150 || timer1 == 160 || timer1 == 170 || timer1 == 180 || timer1 == 190 || timer1 == 200)
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
            if (timer1 == 10 || timer1 == 20 || timer1 == 30 || timer1 == 40 || timer1 == 50 || timer1 == 60 || timer1 == 70 || timer1 == 80 || timer1 == 90 || timer1 == 100 || timer1 == 110 || timer1 == 120 || timer1 == 130 || timer1 == 140
                || timer1 == 150 || timer1 == 160 || timer1 == 170 || timer1 == 180 || timer1 == 190 || timer1 == 200 || timer1 == 210 || timer1 == 220 || timer1 == 230 || timer1 == 240 || timer1 == 250 || timer1 == 260 || timer1 == 270
                || timer1 == 280 || timer1 == 290 || timer1 == 300)
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
            ElectronicEyeDistributePhase = 1;
            maxSpeed = 20f;

            Vector2 offset = npc.Center - Main.player[npc.target].Center;
            offset *= 0.9f;
            Vector2 target = offset.RotatedBy(Main.expertMode ? 0.03f : 0.02f);
            CapVelocity(ref target, 300f);
            Vector2 change = target - offset;
            CapVelocity(ref change, maxSpeed);
            ModifyVelocity(change);
            CapVelocity(ref npc.velocity, maxSpeed);
        }

        private void Lazer(string whattoshoot, Vector2 wheretoshootit)
        {
            if (timer2 >= 10)
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

        private void SpawnMechs()
        {
            SummonTimer++;

            if (SummonTimer >= 275)
            {
                int spawnX = (int)npc.Bottom.X;
                int spawnY = (int)npc.Bottom.Y + 64;
                int Drone1 = NPC.NewNPC(spawnX - 128, spawnY, mod.NPCType("ElectronicEyeDrone"), 0, npc.whoAmI, -1f, 0f, -30f);
                npc.netUpdate = true;
                Main.npc[Drone1].netUpdate = true;
                SummonTimer = 0;
            }

            IdleBehavior();
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
                        reward = mod.ItemType("PhantomBlade");
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

            if (AttackID == 0)
            {
                FrameCycle = 1;
            }
            if (AttackID == 2)
            {
                FrameCycle = 1;
            }
            if (AttackID == 3)
            {
                FrameCycle = 1;
            }
            if (AttackID == 4)
            {
                FrameCycle = 1;
            }

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
            var effects = npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
    }
}