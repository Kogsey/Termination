using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Placeable.Workplaces
{
    public class Mattercondenser : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Finally a way to force Powerful ores into any shape I please.");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 500;
            item.createTile = mod.TileType("Mattercondenser");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dresser);
            recipe.AddIngredient(null, "Araaxiumbar", 10);
            recipe.AddIngredient(null, "BasicCurcuit", 6);
            recipe.AddRecipeGroup("IronBar", 2);
            recipe.AddTile(null, "IWS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}