using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Luminair
{
    [AutoloadEquip(EquipType.Head)]
    public class HardenedLuminiteMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 26;
            item.value = 6000;
            item.rare = ItemRarityID.Purple;
            item.defense = 6;
            item.Termination().extendedrarity = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("luminair alloy mask");
            Tooltip.SetDefault("Increases an Engineer's critical strike chance by 10");
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.09f;

            player.magicCrit += 12;

            player.statManaMax2 += 60;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("HardenedLuminiteBreastplate") && legs.type == mod.ItemType("HardenedLuminiteLeggings");
        }

        public override void UpdateArmorSet(Player p)
        {
            p.setBonus = "Increases jump hight";
            p.jumpBoost = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HardenedAlloy", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}