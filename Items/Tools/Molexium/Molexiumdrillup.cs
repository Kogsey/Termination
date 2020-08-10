using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Tools.Molexium
{
    public class Molexiumdrillup : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molexium shredder");
            Tooltip.SetDefault("That weird tip? \nyeah thats actually physically ripping the atoms connecting the blocks apart \nthen using a high powered ferromagnetic generator to dowse the explosion and power itself!\nAND breath out");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 20;
            item.height = 12;
            item.useTime = 5;
            item.useAnimation = 25;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.pick = 210;
            item.tileBoost++;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 70, 00, 0);
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item23;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Molexiumdrillup");
            item.shootSpeed = 40f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mattercondenser", 10);
            recipe.AddIngredient(null, "Molexiumbar", 12);
            recipe.AddIngredient(null, "Sirenium", 5);
            recipe.AddIngredient(null, "Araaxiumwire", 25);
            recipe.AddTile(null, "Mattercondenser");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}