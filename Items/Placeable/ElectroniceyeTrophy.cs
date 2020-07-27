using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Placeable
{
    public class ElectroniceyeTrophy : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 50000;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("ElectroniceyeTrophy");
            item.placeStyle = 0;
        }
    }
}