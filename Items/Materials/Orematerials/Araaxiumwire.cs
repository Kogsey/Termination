using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Orematerials
{
    public class Araaxiumwire : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Araaxium Wire");
            Tooltip.SetDefault("highly conductive wire");
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
            recipe.AddIngredient(null, "Araaxiumbar", 1);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}