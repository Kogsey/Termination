using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Luminair
{
    [AutoloadEquip(EquipType.Legs)]
    public class HardenedLuminiteLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("luminair alloy Leggings");
            Tooltip.SetDefault("Well padded " +
                "\n6% increased damage " +
                "\n20% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 6000;
            item.rare = ItemRarityID.Purple;
            item.defense = 18;
            item.Termination().extendedrarity = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.20f;

            player.allDamage += 0.06f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HardenedAlloy", 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}