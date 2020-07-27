using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Termination.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Projectiles.BuffProj
{
    public class HardenedLuminiteSetBonusShard : ModProjectile
    {
		private float distance = 50;
		private float RotationSpeed = 0.05f;
        int changetype = 1;
        float changeby = Main.rand.Next(2, 4);


        private float Rotation;


        private bool changecheck1 = false;
        private bool changecheck2 = false;
        private bool changecheck3 = false;
        private bool changecheck4 = false;
        public override void AI()
        {
            Rotation += RotationSpeed;
            projectile.Center = TerminationHelper.PolarPos(Main.player[(int)projectile.ai[0]].Center, distance, MathHelper.ToRadians(Rotation));
            projectile.rotation = (float)(-1 * (TerminationHelper.RotateBetween2Points(Main.player[(int)projectile.ai[0]].Center, projectile.Center) - MathHelper.ToRadians(90)));

            if (Main.player[(int)projectile.ai[0]].HasBuff(ModContent.BuffType<HardenedLuminiteSetBonusBuff>()) == true)
            {
                projectile.timeLeft = 6;
            }

			//change type 1: growing quickly
			//change type 2: growing slowly
			//change type 1: shrinking quickly
			//change type 1: shrinking slowly

			switch (changetype)
			{
				case 1:
                    distance += changeby;
					break;
				case 2:
					distance += changeby;
					break;
				case 3:
					distance -= changeby;
					break;
				case 4:
					distance -= changeby;
					break;
				case 5:
					distance += changeby;
					break;
			}

			if (distance >= 50 && distance < 200 && changecheck1 == false)
			{
				changetype = 1;
				changecheck1 = true;
				changecheck4 = false;
			}

			if (distance >= 200 && distance <= 250 && changecheck2 == false)
			{
				changetype = 2;
				changeby = Main.rand.Next(1, 2);
				changecheck2 = true;
			}

			if (distance > 250 && changecheck3 == false)
			{
				changetype = 3;
				changeby = Main.rand.Next(2, 4);
				changecheck3 = true;
			}

			if (distance < 50 && distance >= 0 && changecheck4 == false)
			{
				changetype = 4;
				changeby = Main.rand.Next(1, 2);
				changecheck4 = true;
			}

			if (distance <= 0)
			{
				changecheck1 = false;
				changecheck2 = false;
				changecheck3 = false;
				changeby = Main.rand.Next(2, 4);
				changetype = 5;
				RotationSpeed = (float)(Main.rand.Next(2, 10) / 10);
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