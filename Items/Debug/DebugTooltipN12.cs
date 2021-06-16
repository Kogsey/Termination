using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Accessories.Debug
{
    public class DebugTooltipN12 : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.IceMirror;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DebugTooltipN12");

        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(34);
            item.rare = ItemRarityID.Blue;
            item.Termination().extendedrarity = ExtendedItemRarity.PostElectronicEyeRare;
        }
    }
}