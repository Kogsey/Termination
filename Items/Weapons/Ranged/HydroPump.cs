using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Ranged
{
    public class HydroPump : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's a high pressure pumping device... do I really need to elaborate?");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.rare = ItemRarityID.Pink;
            item.width = 56;
            item.height = 30;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("HydroPump");
            item.value = Item.sellPrice(gold: 8);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "GolemCurcuit", 3);
            recipe.AddIngredient(mod, "Araaxiumwire", 8);
            recipe.AddIngredient(mod, "Sirenium", 1);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(mod, "IWS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}