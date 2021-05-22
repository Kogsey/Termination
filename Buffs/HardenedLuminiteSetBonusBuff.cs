using Termination.Projectiles.BuffProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Buffs
{
    public class HardenedLuminiteSetBonusBuff : ModBuff
    {
        private int spawntimer = 0;

        private const int Damage = 1000;
        private const float KB = 1;

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Luminite Storm");

        }    

        public override void Update(Player player, ref int buffIndex)
        {
            if (((Entity)player).whoAmI == Main.myPlayer)
            {
                if ((int)player.ownedProjectileCounts[ModContent.ProjectileType<HardenedLuminiteSetBonusShard>()] < 5)
                {
                    if (spawntimer >= 300)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("HardenedLuminiteSetBonusShard"), (int)Damage, KB, player.whoAmI);
                        spawntimer = 0;
                    }
                    else
                    {
                        spawntimer++;
                    }
                }
            }
        }
    }
}