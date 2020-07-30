using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Projectiles.Luminair
{
    public class LuminairSwordProj : ModProjectile
    {
        int dusttimer = 0;

        private int textureversion = Main.rand.Next(1, 3);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminair Shard");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
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

            if (dusttimer >= 5)
            {
                Dust.NewDust(projectile.Center, 3, 3, Terraria.ID.DustID.LunarOre, 0, 0, 0, default, 1);
            }
            else
            {
                dusttimer++;
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

            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                                                                                                                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                                                                                                                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("LuminairDustProj"), 100, 5, Main.myPlayer);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}