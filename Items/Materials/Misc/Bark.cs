using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Misc
{
	public class Bark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bark");
			Tooltip.SetDefault("not usefull in its current state");
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
            recipe.AddIngredient(ItemID.Wood);
			recipe.SetResult(this, 6);
			recipe.AddRecipe();
		}
	}
}
