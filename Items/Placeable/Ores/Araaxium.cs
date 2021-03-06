﻿using Terraria.ID;
using Terraria.ModLoader;

// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"

namespace Termination.Items.Placeable.Ores
{
    public class Araaxium : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Araaxium Ore");
            Tooltip.SetDefault("Seems weak but may be conducive");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("Araaxium");
            item.value = 30;
        }
    }
}