using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Magic
{
    public class Frostbite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrostBite Tome");
            Tooltip.SetDefault("A swirling vortex of temperature somehow avoiding entropy\n" +
                "Oh yes..."
                );
        }

        public override void SetDefaults()
        {
            item.damage = 26;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.mana = 15;
            item.rare = 14;
            item.width = 24;
            item.height = 28;
            item.useTime = 17;
            item.UseSound = SoundID.Item21;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 4.5f;
            item.useAnimation = 17;
            item.shoot = mod.ProjectileType("Frostbite_Proj");
            item.value = 70000;
            item.autoReuse = true;
            item.scale = 0.9f;
            item.knockBack = 5f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType("Frostbite_Proj2");
                item.damage = 13;
            }
            else
            {
                item.shoot = mod.ProjectileType("Frostbite_Proj1");
                item.damage = 26;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flamelash);
            recipe.AddIngredient(ItemID.WaterBolt);
            recipe.AddIngredient(ItemID.FlowerofFrost);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}