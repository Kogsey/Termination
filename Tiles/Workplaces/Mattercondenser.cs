using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ObjectData;
using System;
using Terraria.Enums;

namespace Termination.Tiles.Workplaces
{
	public class Mattercondenser : ModTile
	{		
		public override void SetDefaults()
		{
        	Main.tileFrameImportant[Type] = true;
        	Main.tileLavaDeath[Type] = true;
        	TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        	//TileObjectData.newTile.StyleHorizontal = true;
        	TileObjectData.newTile.StyleWrapLimit = 36;
        	TileObjectData.addTile(Type);
			dustType = mod.DustType("Spark");
        	TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.Table, TileObjectData.newTile.Width, 0);
	    	AddMapEntry(new Color(120, 85, 60));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("Mattercondenser"));
		}
	}
}