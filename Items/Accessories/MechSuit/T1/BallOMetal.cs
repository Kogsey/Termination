using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Accessories.MechSuit.T1
{
    public class BallOMetal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 44;
            item.value = 250000;
            item.rare = ItemRarityID.Purple;
            item.Termination().extendedrarity = 12;
            item.accessory = true;
            item.defense = 4;
            item.expertOnly = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ball 'O' metal");
            Tooltip.SetDefault("Grants a spinning ball around the player");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(mod.BuffType("BallOMetalBuff"), 5);
        }
    }
}