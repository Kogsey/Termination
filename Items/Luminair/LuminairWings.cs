using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Termination.Items.Luminair
{
	[AutoloadEquip(EquipType.Wings)]
	public class LuminairWings : ModItem
	{

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This is a modded wing.");
			DisplayName.SetDefault("Luminair wings");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.wingTimeMax = 200;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend) {
			ascentWhenFalling = 1f;
			ascentWhenRising = 0.2f;
			maxCanAscendMultiplier = 1.25f;
			maxAscentMultiplier = 3.25f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration) {
			speed = 10f;
			acceleration *= 3f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "HardenedAlloy", 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}