using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Banners
{
    public class mechslimeBanner : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = mod.TileType("MonsterBanner");
            item.placeStyle = 44;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("mechslimeBanner");
            Tooltip.SetDefault("");
        }
    }
}