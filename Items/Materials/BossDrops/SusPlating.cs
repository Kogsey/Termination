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
			Tooltip.SetDefault("Not a really usefull item but can be used"
			+ "conductive");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Purple;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Araaxium", 4);
			recipe.AddTile(mod, "Heatfurn");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
