using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Bars
{
	public class HardenedAlloy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName
			.SetDefault("Hardened luminite alloy");
			Tooltip.SetDefault("unnervingly tough");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 3000;
			item.rare = ItemRarityID.Purple;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Sirenium", 7);
			recipe.AddIngredient(null, "SusPlating", 1);
			recipe.AddTile(ItemID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
