using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Projectiles.BuffProj
{
    public class HardenedLuminiteSetBonusShard : ModProjectile
    {
        private const float Distanse = 50;
        private const float RotationSpeed = 0.05f;
        private float Rotation;

        public override void AI()
        {
            Rotation += RotationSpeed;
            projectile.Center = TerminationHelper.PolarPos(Main.player[(int)projectile.ai[0]].Center, Distanse, MathHelper.ToRadians(Rotation));
            projectile.rotation = (float)(-1 * (TerminationHelper.RotateBetween2Points(Main.player[(int)projectile.ai[0]].Center, projectile.Center) - MathHelper.ToRadians(90)));

            if (Main.player[(int)projectile.ai[0]].HasBuff(ModContent.BuffType<HardenedLuminiteSetBonusBuff>()) == true)
            {
                projectile.timeLeft = 6;
            }
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

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.timeLeft = 6;
            projectile.melee = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminite Shard");
        }
    }
}