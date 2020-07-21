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

namespace Termination.Items.Materials.Misc
{
	public class GolemCurcuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName
			.SetDefault("Lihzahrd Circuit");
			Tooltip.SetDefault("it's no wonder that Golem Has existed through the ages with tech like this.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 60000;
			item.rare = ItemRarityID.Green;
		}
	}
}
