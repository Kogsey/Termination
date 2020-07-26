using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Armour.HardenedLuminite
{
	[AutoloadEquip(EquipType.Head)]
	public class HardenedLuminiteHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("luminair alloy helmet");
			Tooltip.SetDefault("The impenetrable shield " +
				"\n3% increased Damage " +
				"\n17% increased melee crit chance " +
				"\n20% increased melee speed");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 26;
			item.value = 6000;
			item.rare = ItemRarityID.Purple;
			item.defense = 6;
			item.Termination().extendedrarity = 12;
		}
		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.03f;

			player.meleeCrit += 17;

			player.meleeSpeed += 20;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("HardenedLuminiteBreastplate") && legs.type == mod.ItemType("HardenedLuminiteLeggings");
		}
		public override void UpdateArmorSet(Player p)
		{
			p.setBonus = "Enimies are more likely to target you";

            p.jumpBoost = true;

			p.aggro += 1000;
        }
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowSubtle = true;
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
