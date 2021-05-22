using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Projectiles.Melee.Luminair
{
    public class LuminairDustProj : ModProjectile
    {

        private int textureversion = Main.rand.Next(1, 3);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminair Speck");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.timeLeft = Main.rand.Next(30, 60);
            projectile.melee = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            switch (textureversion)
            {
                case 1:
                    projectile.frame = 1;
                    break;

                case 2:
                    projectile.frame = 2;
                    break;

                case 3:
                    projectile.frame = 3;
                    break;
            }

            projectile.rotation = projectile.velocity.ToRotation() + ((float)-1.5 * MathHelper.PiOver2);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.Center, 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}