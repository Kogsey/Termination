using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Ranged.Ammo
{
    public class EyeC : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DNA Capsule:Eye");
            Tooltip.SetDefault("A capsule containing a strips of DNA ready to be launched.");
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.ranged = true;
            item.width = 14;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("EyeC");
            item.shootSpeed = 6f;
            item.ammo = mod.ItemType("BeeC");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Hive);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}