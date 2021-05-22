using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Placeable.Workplaces
{
    public class IWS : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Intricate Work Station");
            Tooltip.SetDefault("Intricate: very complicated or detailed; I think this applies.");
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
            item.value = 2060000;
            item.createTile = mod.TileType("IWS");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WorkBench);
            recipe.AddIngredient(null, "BasicCurcuit", 1);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.CopperBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}