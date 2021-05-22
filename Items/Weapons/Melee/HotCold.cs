using Termination.Projectiles.Melee.Boomerangs;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Melee
{
    public class HotCold : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot 'N' Cold");
            Tooltip.SetDefault("The impressive connection of freezing and boiling");
        }

        public override void SetDefaults()
        {
            item.damage = 43;
            item.shootSpeed = 20f;
            item.melee = true;
            item.width = 9;
            item.height = 16;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 8;
            item.value = 1000;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<ProjHotCold>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flamarang, 1);
            recipe.AddIngredient(ItemID.IceBoomerang, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}