using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Armour
{
    [AutoloadEquip(EquipType.Body)]
    public class HardenedLuminiteBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("luminair alloy Breastplate");
            Tooltip.SetDefault("Tough yet light " +
                "\n30% Damage " +
                "\n16% crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 6000;
            item.rare = ItemRarityID.Purple;
            item.defense = 32;
            item.Termination().extendedrarity = ExtendedItemRarity.PostElectronicEye;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.30f;

            player.meleeCrit += 16;
            player.rangedCrit += 16;
            player.magicCrit += 16;
            player.thrownCrit += 16;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HardenedAlloy", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}