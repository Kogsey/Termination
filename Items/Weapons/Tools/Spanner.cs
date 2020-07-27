using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Tools
{
    public class Spanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName
            .SetDefault("Spanner");
            Tooltip.SetDefault("Not that great as a weapon, but pretty good for crafting.");
        }

        public override void SetDefaults()
        {
            item.damage = 28;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = 1000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}