using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs.Bosses.ElectronicEye
{
    public class ElectronicEyeDrone : ModNPC
    {
        private float maxSpeed = 50f;
        private float timer1 = 0f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drone:Anchor Head - Head Must Survive");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50000;
            npc.damage = 100;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = false;
            npc.width = 48;
            npc.height = 48;
            npc.alpha = 1;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
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
            NPC headNPC = Main.npc[(int)npc.ai[0]];
            if (!headNPC.active || headNPC.type != mod.NPCType("ElectronicEye"))
            {
                npc.active = false;
                return;
            }
            npc.timeLeft = headNPC.timeLeft;

            if (!npc.HasValidTarget)
            {
                npc.TargetClosest(false);
            }

            npc.position = new Vector2(Main.player[npc.target].Center.X - npc.width / 2, Main.player[npc.target].Center.Y - 200);

            AttackTimer++;
            if (AttackTimer % 120 == 0)
            {
                Lazer("ElectronicEyeThornBall", Main.player[npc.target].Center);
            }
        }

        private void Spin()
        {
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
            int k = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
            Main.projectile[k].friendly = false;
            Main.projectile[k].hostile = true;
            Main.projectile[k].scale = 1f;
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

        public virtual void CreateDust(Vector2 dustlocation)
        {
            
            Dust.NewDust(dustlocation, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
            List<Vector2> vectors = new List<Vector2>();
            for(int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                    vectors.Add(new Vector2(dustlocation.X + (float)(i * Main.rand.Next(1, 50)), dustlocation.Y + (float)(j * Main.rand.Next(1, 50))));
            }

            foreach (Vector2 v in vectors)
                Dust.NewDust(v, npc.width, npc.height, mod.DustType("MotherSpark"), 0f, 0f, 1, default, 1f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 1f;
        }

        private static float NewMethod(Texture2D texture)
        {
            return texture.Height;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Termination/NPCs/Bosses/ElectronicEye/BallMetal_Chain");

            Vector2 position = npc.Center;
            Vector2 mountedCenter = Head.npc.Center;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = NewMethod(texture);
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