using Terraria.ID;
using Terraria.ModLoader;

// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"

namespace Termination.Items.Placeable.Ores
{
    public class Sirenium : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName
            .SetDefault("Sirenium Ore");
            Tooltip.SetDefault("So light it slightly pulls you upwards.\n" +
                "Probably useless unless combined with something heavier.");
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
            item.createTile = mod.TileType("Sirenium");
        }
    }
}