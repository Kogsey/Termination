using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Misc
{
    public class Coolant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coolant");
            Tooltip.SetDefault("A glass of liquid coolant");
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
            recipe.AddIngredient(ItemID.Glass, 2);
            recipe.AddIngredient(ItemID.Gel, 30);
            recipe.AddIngredient(ItemID.WaterBucket);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
        }
    }
}