using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs.Bosses.ElectronicEye
{
    public class BallMetal2 : ModNPC
    {
        private float timer1 = 0;
        private float shoottimer1 = 5f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MACE1");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50000;
            npc.damage = 100;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = true;
            npc.width = 80;
            npc.height = 80;
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

            if (Head.ElectronicEyeDistributePhase <= 1)
            {
                Spin();
            }
            else if (Head.ElectronicEyeDistributePhase == 2)
            {
                Mace();
            }
            else if (Head.ElectronicEyeDistributePhase == 3)
            {
                Spin();
                npc.dontTakeDamage = false;
            }
        }

        private void Spin()
        {
            npc.Center = Head.BallMetalCenters[1];
            npc.rotation = TerminationUtils.RotateBetween2Points(Main.player[(int)npc.ai[0]].Center, npc.Center) - MathHelper.ToRadians(90);
        }

        private void Mace()
        {
            timer1++;
            if (timer1 >= 120)
            {
                Vector2 target = Head.BallMetalCenters[1];
                npc.rotation = TerminationUtils.RotateBetween2Points(Main.player[(int)npc.ai[0]].Center, npc.Center) - MathHelper.ToRadians(90);
                Vector2 shootVel = target - npc.Center;
                shootVel.Normalize();
                shootVel *= 5f;
                npc.velocity = shootVel;

                Lazer("ElectronicEyeBeam", Main.player[npc.target].Center);
            }
            if (timer1 >= 359)
            {
                timer1 = 0;
            }
        }

        private void Lazer(string whattoshoot, Vector2 wheretoshootit)
        {
            if (shoottimer1 >= 10)
            {
                Vector2 shootPos = npc.Center;
                float inaccuracy = 3f * (npc.life / npc.lifeMax);
                Vector2 shootVel = wheretoshootit - npc.Center + new Vector2(Main.rand.NextFloat(-inaccuracy, inaccuracy), Main.rand.NextFloat(-inaccuracy, inaccuracy));
                shootVel.Normalize();
                shootVel *= 28f;
                int k = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType(whattoshoot), npc.damage / 2, 5f, Main.myPlayer);
                Main.projectile[k].friendly = false;
                Main.projectile[k].hostile = true;
                Main.projectile[k].scale = 1f;
                shoottimer1 = 0;
            }
            else
            {
                shoottimer1 += 1;
            }
        }

        private void CapVelocity(ref Vector2 velocity, float max)
        {
            if (velocity.Length() > max)
            {
                velocity.Normalize();
                velocity *= max;
            }
        }

        private void ModifyVelocity(Vector2 modify, float weight = 0.05f)
        {
            npc.velocity = Vector2.Lerp(npc.velocity, modify, weight);
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

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(Main.npcTexture[Head.npc.type], Head.npc.Center - Main.screenPosition + new Vector2(0, Head.npc.gfxOffY), Head.npc.frame,
                             Color.White, Head.npc.rotation, Head.npc.frame.Size() / 2, Head.npc.scale, SpriteEffects.None, 1f);
        }
    }
}