using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Placeable.Workplaces
{
    internal class MagicExtractor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Extractor");
            Tooltip.SetDefault("A device for extracting magic from ancient artifacts");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Furnace);
            item.createTile = mod.TileType("MagicExtractor");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm);
            recipe.AddIngredient(null, "Araaxiumwire", 3);
            recipe.AddIngredient(null, "AdvancedCurcuit", 6);
            recipe.AddRecipeGroup("IronBar", 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}