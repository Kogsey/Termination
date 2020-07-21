using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T2
{
	public class EnHeart1 : ModItem
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
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperBar, 8);
            recipe.AddIngredient(null, "EnEngineer");
            recipe.AddIngredient(null, "EnMagic");
            recipe.AddIngredient(null, "EnMelee");
            recipe.AddIngredient(null, "EnRanged");
            recipe.AddIngredient(null, "EnSummoner");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
