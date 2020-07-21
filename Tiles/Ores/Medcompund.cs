using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Tiles.Ores
{
	public class Medcompund : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType("YellowOreDust");
			drop = mod.ItemType("Medcompund");
			AddMapEntry(new Color(0, 0, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 3f;
			g = 3f;
			b = 3f;
		}
	}
}