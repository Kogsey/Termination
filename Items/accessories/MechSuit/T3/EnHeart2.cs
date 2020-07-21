using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T3
{
	public class EnHeart2 : ModItem
	{

		public override void SetStaticDefaults()
		{
            
			Tooltip.SetDefault("5% increased Engineer damage" +
			                   "\n10% increased Engineer critical strike chance" +
			                   "\n20 increased increased Engineer knockback");
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
			modPlayer.EngineerDamage += 5f;
			modPlayer.EngineerCrit += 10;
			modPlayer.EngineerKnockback += 5;
            player.AddBuff(mod.BuffType("BallOMetalBuff2"), 3);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "BallOMetal");
            recipe.AddIngredient(null, "MLMagic");
            recipe.AddIngredient(null, "EnHeart1");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}