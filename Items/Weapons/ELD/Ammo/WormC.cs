using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.ELD.Ammo
{
	public class WormC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Advanced DNA Capsule:Worms");
			Tooltip.SetDefault("A capsule containing a strips of DNA ready to be launched.");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.ranged = false;
			item.width = 14;
			item.height = 14;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = ItemRarityID.Yellow;
			item.shoot = mod.ProjectileType("WormC");
			item.shootSpeed = 4f;   
			item.ammo = mod.ItemType("BeeC");
		}

		public override void AddRecipes()
		{
			ToolRecipe recipe = new ToolRecipe(mod);
			recipe.AddIngredient(ItemID.Hive);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}