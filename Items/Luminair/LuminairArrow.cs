using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Termination.Projectiles.Luminair;
using static Terraria.ModLoader.ModContent;
using Termination.Items.Materials.Bars;
using Termination.Tiles.Workplaces;

namespace Termination.Items.Luminair
{
	public class LuminairArrow : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This is a modded bullet ammo.");
		}

		public override void SetDefaults() {
			item.damage = 17;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.Termination().extendedrarity = 12;
			item.shoot = ProjectileType<Projectiles.Luminair.LuminairArrowProj>();   //The projectile shoot when your weapon using this ammo
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<HardenedAlloy>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
