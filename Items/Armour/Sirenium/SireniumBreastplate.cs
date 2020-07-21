using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Armour.Sirenium
{
	[AutoloadEquip(EquipType.Body)]
	public class SireniumBreastplate : ModItem
	{

		public override void SetDefaults()
		{

			item.width = 26;
			item.height = 18;

			item.value = 6000;
			item.rare = ItemRarityID.Orange;
			item.defense = 7;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heaven Breastplate");
			Tooltip.SetDefault("Absolutly nothing special... but the set bonus increses jump hight if you care");
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Sireniumbar", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
