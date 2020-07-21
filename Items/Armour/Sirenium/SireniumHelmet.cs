using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Armour.Sirenium
{
	[AutoloadEquip(EquipType.Head)]
	public class SireniumHelmet : ModItem
	{
        public override void SetDefaults()
		{

			item.width = 32;
			item.height = 26;

			item.value = 6000;
			item.rare = ItemRarityID.Orange;
			item.defense = 6;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sirenium visor");
			Tooltip.SetDefault("Increases an Engineer's critical strike chance by 10");
		}

		public override void UpdateEquip(Player player)
		{
            TerminationPlayer modPlayer = TerminationPlayer.ModPlayer(player);
            modPlayer.EngineerCrit += 10;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("SireniumBreastplate") && legs.type == mod.ItemType("SireniumLeggings");
		}

		public override void UpdateArmorSet(Player p)
		{
			p.setBonus = "Increases jump hight";
            p.jumpBoost = true;
        }

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowSubtle = true;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Sireniumbar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
