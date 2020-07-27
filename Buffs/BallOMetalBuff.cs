using Terraria;
using Terraria.ModLoader;

namespace Termination.Buffs
{
    public class BallOMetalBuff : ModBuff
    {
        private int MinionType = -1;
        private int MinionID = -1;

        private const int Damage = 6;
        private const float KB = 1;

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Ball 'O' Metal");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (MinionType == -1)
                MinionType = mod.ProjectileType("BallOMetalPro");
            if (MinionID == -1 || Main.projectile[MinionID].type != MinionType || !Main.projectile[MinionID].active || Main.projectile[MinionID].owner != player.whoAmI)
                MinionID = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, MinionType, (int)(Damage * player.meleeDamage), KB, player.whoAmI);
            else
                Main.projectile[MinionID].timeLeft = 6;
        }
    }
}