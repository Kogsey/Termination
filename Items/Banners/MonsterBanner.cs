using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Termination.Items.Banners
{
	public class MonsterBanner : ModTile
{
    public override void SetDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = new[]{16, 16, 16};
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleWrapLimit = 111;
        TileObjectData.addTile(Type);
        dustType = -1;
	AddMapEntry(new Color(13, 88, 130));
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int style = frameX / 2;
        string item;
        switch(style)
        {
        case 0:
            item = "mechslimeBanner";
            break;
        case 1:
            item = "SpringJumpBanner";
            break;
        default:
            return;
        }
        Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType(item));
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if(closer)
        {
            Player player = Main.player[Main.myPlayer];
            int style = Main.tile[i, j].frameX / 2;
            string type;
            switch(style)
        {
        case 0:
            type = "mechslimeBanner";
            break;
        case 1:
            type = "SpringJumpBanner";
            break;
        default:
            return;
        }
            player.NPCBannerBuff[mod.NPCType(type)] = true;
            player.hasBanner = true;
        }
    }
}}