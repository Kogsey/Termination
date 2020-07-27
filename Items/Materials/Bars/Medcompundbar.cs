using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.Bars
{
    public class Medcompundbar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName
            .SetDefault("medium compound Bar");
            Tooltip.SetDefault("A marvelous alloy between molexium and sirenium");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 5000;
            item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Medcompund", 10);
            recipe.AddTile(mod, "Mattercondenser");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}