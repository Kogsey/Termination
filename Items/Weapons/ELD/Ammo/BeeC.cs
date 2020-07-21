using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.ELD.Ammo
{
	public class BeeC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DNA Capsule:Bees");
			Tooltip.SetDefault("A capsule containing a strips of DNA ready to be launched. Who needs mana huh");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.ranged = false;
			item.width = 14;
			item.height = 14;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = ItemRarityID.Orange;
			item.shoot = mod.ProjectileType("BeeC");
			item.shootSpeed = 4f;   
			item.ammo = item.type;
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

	public class ToolRecipe : ModRecipe
	{
		public ToolRecipe(Mod mod) : base(mod)
		{
		}

		public override bool RecipeAvailable()
		{
			return Main.LocalPlayer.HasItem(mod.ItemType("EngineerTool"));
		}
	}
}
