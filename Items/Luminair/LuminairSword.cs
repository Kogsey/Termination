using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Termination.Projectiles.Luminair;
using static Terraria.ModLoader.ModContent;

namespace Termination.Items.Luminair
{
	public class LuminairSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminair broadsword");
			Tooltip.SetDefault("one of these days I'm going to have a stick of my own");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<LuminairSwordProj>();
			item.shootSpeed = 5f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "HardenedAlloy", 15);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) 
		{
				target.AddBuff(mod.BuffType(""), 60);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			// Fix the speedX and Y to point them horizontally.
			// Add random Rotation
			Vector2 speed = new Vector2(speedX, speedY);
			speed = speed.RotatedByRandom(MathHelper.ToRadians(30));
			// Change the damage since it is based off the weapons damage and is too high
			damage = (int)(damage * .1f);
			return true;
		}
	}
}