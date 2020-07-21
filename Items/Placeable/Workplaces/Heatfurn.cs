using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Placeable.Workplaces
{
	class Heatfurn : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Electric Heated Furnace");
			Tooltip.SetDefault("uses power to craft");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Furnace);
			item.createTile = mod.TileType("Heatfurn");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Hellforge);
			recipe.AddIngredient(null, "Araaxium", 3);
			recipe.AddIngredient(null, "BasicCurcuit", 6);
			recipe.AddRecipeGroup("IronBar", 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}