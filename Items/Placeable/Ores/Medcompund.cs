using Terraria.ID;
using Terraria.ModLoader;

// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"

namespace Termination.Items.Placeable.Ores
{
    public class Medcompund : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName
            .SetDefault("Medium density compund");
            Tooltip.SetDefault("Exactly what it says on the tin, it's slightly unstable but should safe.");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("Medcompund");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Molexium", 1);
            recipe.AddIngredient(null, "Sirenium", 1);
            recipe.AddTile(mod, "Mattercondenser");
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}