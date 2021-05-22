using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs.Bosses.ElectronicEye
{
    public class ElectronicEyeAnchor : ModNPC
    {
        private float maxSpeed = 50f;
        private float timer1 = 0f;
        private float shoottimer1 = 0f;
        private float timeuntilteleport = 0f;
        private bool check1 = false;
        private bool teleportlocationcheck = false;
        private double Healthframes = 0f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drone:Anchor Head - Head Must Survive");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 100000;
            npc.damage = 100;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = false;
            npc.width = 64;
            npc.height = 64;
            npc.alpha = 1;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.7f);
        }

        public ElectronicEye Head
        {
            get
            {
                return (ElectronicEye)Main.npc[(int)npc.ai[0]].modNPC;
            }
        }

        public float Direction
        {
            get
            {
                return npc.ai[1];
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

        public float MaxAttackTimer
        {
            get
            {
                return 60f + 120f * (float)Head.npc.life / (float)Head.npc.lifeMax;
            }
        }

        public override void AI()
        {
            AttackTimer++;
            if (AttackTimer >= MaxAttackTimer)
            {
                AttackID++;
                AttackTimer = 0;
            }
            if (AttackID > 3)
                AttackID = Main.rand.Next(0, 3);
            else if (AttackID == 1)
            {
                InnacurateLazerUp("ElectronicEyeBeam", Main.player[npc.target].Center);
            }
            else if (AttackID == 0)
            {
                AttackID = 1;
            }
        }

        private void InnacurateLazerUp(string whattoshoot, Vector2 wheretoshootit)
        {
            Healthframes = (double)npc.life / (double)npc.lifeMax * 10f;
            if (shoottimer1 >= Healthframes)
            {
                float inaccuracy = 3f * (npc.life / npc.lifeMax);
                Vector2 shootVel = wheretoshootit - npc.Center + new Vector2(Main.rand.NextFloat(-inaccuracy, inaccuracy), Main.rand.NextFloat(-inaccuracy, inaccuracy));
                shootVel.Normalize();
                shootVel *= 28f;
                int i = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[i].friendly = false;
                Main.projectile[i].hostile = true;
                Main.projectile[i].scale = 1f;
                shoottimer1 = 0;
            }
            else
            {
                shoottimer1 += 1;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Termination/NPCs/Bosses/ElectronicEye/BallMetal_Chain");

            Vector2 position = npc.Center;
            Vector2 mountedCenter = Head.npc.Center;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
             Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
            bool flag = !(float.IsNaN(position.X) && float.IsNaN(position.Y));
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if (vector2_4.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = npc.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}