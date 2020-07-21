using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Misc
{
	public class DeadMagicExtract : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul's essence");
			Tooltip.SetDefault("A tortured soul put to use");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm);
            recipe.AddTile(mod, "MagicExtractor");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
