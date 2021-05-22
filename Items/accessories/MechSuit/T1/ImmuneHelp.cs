using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Accessories.MechSuit.T1
{
    public class ImmuneHelp : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing Nano-bot Mix");
            Tooltip.SetDefault("Increases life regeneration\n" +
                               "Makes you immune to enemy nano-bots");
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
            player.buffImmune[mod.BuffType("Nanobot")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(ItemID.Bezoar, 1);
            recipe.AddIngredient(ItemID.BandofRegeneration, 1);
            recipe.AddTile(null, "IWS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}