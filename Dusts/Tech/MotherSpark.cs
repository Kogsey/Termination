using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Dusts.Tech
{
    public class MotherSpark : ModDust
    {
        private Vector2 dustlocation1;
        private Vector2 dustlocation2;
        private Vector2 dustlocation3;
        private Vector2 dustlocation4;

        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.4f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale *= 1.5f;

            dustlocation1.X = dust.position.X + Main.rand.Next(1, 50);
            dustlocation1.Y = dust.position.Y + Main.rand.Next(1, 50);
            dustlocation2.X = dust.position.X - Main.rand.Next(1, 50);
            dustlocation2.Y = dust.position.Y - Main.rand.Next(1, 50);
            dustlocation3.X = dust.position.X + Main.rand.Next(1, 50);
            dustlocation3.Y = dust.position.Y - Main.rand.Next(1, 50);
            dustlocation4.X = dust.position.X - Main.rand.Next(1, 50);
            dustlocation4.Y = dust.position.Y + Main.rand.Next(1, 50);

            Dust.NewDust(dust.position, 8, 8, mod.DustType("Spark"), 0f, 0f, 1, default(Color), 1f);
            Dust.NewDust(dustlocation1, 8, 8, mod.DustType("Spark"), 0f, 0f, 1, default(Color), 1f);
            Dust.NewDust(dustlocation2, 8, 8, mod.DustType("Spark"), 0f, 0f, 1, default(Color), 1f);
            Dust.NewDust(dustlocation3, 8, 8, mod.DustType("Spark"), 0f, 0f, 1, default(Color), 1f);
            Dust.NewDust(dustlocation4, 8, 8, mod.DustType("Spark"), 0f, 0f, 1, default(Color), 1f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f;
            float light = 0.35f * dust.scale;
            Lighting.AddLight(dust.position, light, light, light);
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}