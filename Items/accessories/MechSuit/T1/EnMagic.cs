using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T1
{
    public class EnMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Magic");
            Tooltip.SetDefault("Increses mana regen twice as mutch as the band of mana regeneration" +
                               "plus 40 mana" +
                               "1 defence");
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
            player.manaRegen += 1;
            player.statManaMax2 += 40;
            player.statDefense += 1;
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(ItemID.ManaRegenerationBand, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}