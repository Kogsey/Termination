using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T1
{
	public class EnEngineer : ModItem
	{

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Band of the Engineer");
			Tooltip.SetDefault("Increses life regen twice as mutch as the band of regeneration" +
			                   " makes you immune to nanobots" +
			                   "2 defence");
		}

		public override void SetDefaults()
		{
			item.Size = new Vector2(34);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			TerminationPlayer modPlayer = TerminationPlayer.ModPlayer(player);
            player.lifeRegen += 1;
            player.buffImmune[mod.BuffType("Nanobot")] = true;
            player.statDefense += 2;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(null,"ImmuneHelp", 5);
            recipe.AddIngredient(ItemID.Bezoar, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
