using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Projectiles.Luminair
{
    public class LuminairArrowProj : ModProjectile
    {
        int speedtimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminair arrow");
        }

        public override void SetDefaults()
        {
            projectile.width = 8;      
            projectile.height = 8;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (speedtimer == 0)
            {
                projectile.velocity /= 5;
            }

            if (speedtimer >= 30)
            {
                Dust.NewDust(projectile.Center, 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
                projectile.velocity *= 2;
                speedtimer = 1;

                for (int index1 = 0; index1 < 12; ++index1)
                {
                    Vector2 vector2 = Utils.RotatedBy(Vector2.UnitX * - projectile.width / 2f + -Utils.RotatedBy(Vector2.UnitY, index1 * MathHelper.Pi / 6.0, new Vector2()) * new Vector2(8f, 16f), projectile.rotation - MathHelper.PiOver2, new Vector2());
                    int index2 = Dust.NewDust(projectile.Center, 0, 0, DustID.LunarOre, 0.0f, 0.0f, 160, new Color(), 1f);
                    ((Dust)Main.dust[index2]).scale = 1.1f;
                    ((Dust)Main.dust[index2]).noGravity = true;
                    ((Dust)Main.dust[index2]).position = (projectile.Center + vector2);
                    ((Dust)Main.dust[index2]).velocity = (projectile.velocity * 0.1f);
                    ((Dust)Main.dust[index2]).velocity = Vector2.Normalize(projectile.Center - (projectile.velocity * 3f - (Main.dust[index2]).position) * 1.25f);
                }
            }
            else
            {
                speedtimer++;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.Center, 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
            Dust.NewDust(projectile.Center + new Vector2(Main.rand.Next(-4, 4)), 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
            Dust.NewDust(projectile.Center + new Vector2(Main.rand.Next(-4, 4)), 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
            Dust.NewDust(projectile.Center + new Vector2(Main.rand.Next(-4, 4)), 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
            Dust.NewDust(projectile.Center + new Vector2(Main.rand.Next(-4, 4)), 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}