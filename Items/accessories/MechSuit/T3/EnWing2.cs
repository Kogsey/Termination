using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.accessories.MechSuit.T3
{
	[AutoloadEquip(EquipType.Wings)]
	public class EnWing2 : ModItem
	{

		public override void SetDefaults()
		{

			item.width = 22;
			item.height = 20;

			item.value = 125000;
			item.rare = ItemRarityID.Purple;
			item.accessory = true;

		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Engineer's wings");
			Tooltip.SetDefault("An enginner likes typos");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.wingTimeMax = 200;
            TerminationPlayer modPlayer = TerminationPlayer.ModPlayer(player);
            modPlayer.EngineerDamage += 5f;
            modPlayer.EngineerCrit += 10;
            modPlayer.EngineerKnockback += 5;
        }

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{

			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.155f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 15f;
			acceleration *= 2.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "Molexiumbar", 8);
            recipe.AddIngredient(null, "MLMagic", 5);
            recipe.AddIngredient(null, "EnWing", 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
