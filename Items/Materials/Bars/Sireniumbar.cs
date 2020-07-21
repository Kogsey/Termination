using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Bars
{
	public class Sireniumbar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName
			.SetDefault("Sirenium Bar");
			Tooltip.SetDefault("since it's been condensed it no longer makes you float");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 3000;
			item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Sirenium", 7);
			recipe.AddTile(mod, "Mattercondenser");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
