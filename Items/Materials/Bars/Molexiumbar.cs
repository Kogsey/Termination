using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Bars
{
	public class Molexiumbar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName
			.SetDefault("Molexium Bar");
			Tooltip.SetDefault("I'm not sure if I should be impressed or scared");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 3000;
			item.rare = ItemRarityID.Red;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Molexium", 7);
			recipe.AddTile(mod, "Mattercondenser");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
