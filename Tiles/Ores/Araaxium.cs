using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Tiles.Ores
{
	public class Araaxium : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType("PurpleOreDust");
			drop = mod.ItemType("Araaxium");
			AddMapEntry(new Color(61, 0, 55));
mineResist = 12f;
minPick = 110;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 1.5f;
			g = 1.5f;
			b = 1.5f;
		}
	}
}