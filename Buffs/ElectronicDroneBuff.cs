using Termination;
using Termination.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Termination.Buffs
{
	public class ElectronicDroneBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Purity Wisp");
			Description.SetDefault("The purity wisp will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerminationPlayer modPlayer = player.GetModPlayer<TerminationPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<ElectronicDrone>()] > 0) {
				modPlayer.ElectronicDrone = true;
			}
			if (!modPlayer.ElectronicDrone) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}