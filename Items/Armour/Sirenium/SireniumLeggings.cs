using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Armour.Sirenium
{
	[AutoloadEquip(EquipType.Legs)]
	public class SireniumLeggings : ModItem
	{

		public override void SetDefaults()
		{

			item.width = 22;
			item.height = 18;
			item.value = 6000;

			item.rare = ItemRarityID.Orange;
			item.defense = 5;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sirenium Leggings");
			Tooltip.SetDefault("40% incresed movement speed");
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.40f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Sireniumbar", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
