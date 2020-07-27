using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T1
{
    public class EnRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Ranged");
            Tooltip.SetDefault("Increses life regen twice as mutch as the band of regeneration" +
                               "makes you immune to poisen" +
                               "2 defence");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(34);
            item.rare = ItemRarityID.Green;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            TerminationPlayer modPlayer = TerminationPlayer.ModPlayer(player);
            player.lifeRegen += 1;
            player.buffImmune[20] = true;
            player.statDefense += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(ItemID.BandofRegeneration, 1);
            recipe.AddIngredient(ItemID.Bezoar, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}