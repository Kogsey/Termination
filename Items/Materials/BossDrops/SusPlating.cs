using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.BossDrops
{
	public class SusPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hardened Luminite plating");
			Tooltip.SetDefault("seems to be impossible to forge and yet here it is"
			+ "could be mixed with something weaker");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Purple;
		}
	}
}
