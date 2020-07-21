using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Misc
{
	public class Flesh : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goey Flesh");
			Tooltip.SetDefault("This red amalgamation fell from the wall of flesh... Still beating");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
		}
	}
}
