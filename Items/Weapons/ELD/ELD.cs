using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.ELD
{
	public class ELD : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName
			.SetDefault("Entity Launching Handgun");
			Tooltip.SetDefault("Yeet that DNA");
		}

		public override void SetDefaults()
		{
			item.damage = 45;
			item.width = 42;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item116;
			item.autoReuse = true;
			item.shootSpeed = 1f;
			item.shoot = ProjectileID.PurificationPowder;
			item.useAmmo = mod.ItemType("BeeC");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.BeeWax, 30);
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddIngredient(ItemID.Vertebrae, 30);
			recipe.AddIngredient(null, "BasicCurcuit", 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		[System.Obsolete]
		public override void GetWeaponDamage(Player player, ref int damage)
		{
			damage = (int)(damage * player.bulletDamage + 5E-06);
		}

		public override Vector2? HoldoutOffset()
		{
			return Vector2.Zero;
		}
	}
}