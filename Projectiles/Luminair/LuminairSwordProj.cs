using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Projectiles.Luminair
{
    public class LuminairSwordProj : ModProjectile
    {
        private float distance = 50;
        private float RotationSpeed = 0.05f;
        private int changetype = 1;
        private float changeby = Main.rand.Next(2, 4);

        private float Rotation;

        private bool changecheck1 = false;
        private bool changecheck2 = false;
        private bool changecheck3 = false;
        private bool changecheck4 = false;

        private int textureversion = Main.rand.Next(1, 3);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminite Shard est");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.timeLeft = 60;
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

            projectile.rotation = projectile.velocity.ToRotation() + ((float)-1.5 * MathHelper.PiOver2)
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void Kill(int timeLeft)
        {

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}