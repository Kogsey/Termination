using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T2
{
    public class EnTreaders : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer's Boots");
            Tooltip.SetDefault("An Engineer needs tough boots for a work enviroment!!!");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 28;
            item.value = 100;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.waterWalk = true;
            player.buffImmune[24] = true;
            player.maxRunSpeed += 4f;
            player.moveSpeed += 1.75f;
            player.wingTimeMax += 100;
            player.jumpSpeedBoost += 3.5f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
                recipe.AddIngredient(ItemID.LavaWaders, 1);
                recipe.AddIngredient(null, "ElectronicEyeMask", 5);
                recipe.AddIngredient(null, "Coolant", 20);
                recipe.AddIngredient(null, "AdvancedCurcuit", 5);
                recipe.AddTile(mod, "IWS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}