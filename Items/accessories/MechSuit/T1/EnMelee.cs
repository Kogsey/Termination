using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Accessories.MechSuit.T1
{
    public class EnMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Melee");
            Tooltip.SetDefault("Increases life regeneration twice as much as the band of regeneration\n" +
                               "makes you immune to poison\n" +
                               "3 defense");
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
            player.statDefense += 3;
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