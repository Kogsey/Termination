using Microsoft.Xna.Framework;
using Termination.Buffs;
using Termination.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Termination.Items.Weapons.Summon
{
	public class DamagedControlCircuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Damaged control circuit");
			Tooltip.SetDefault("Held together by lint and barely working, but work it does");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 110;
			item.summon = true;
			item.mana = 10;
			item.width = 26;
			item.height = 28;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item44;
			item.shoot = ProjectileType<ElectronicDrone>();
			item.buffType = BuffType<ElectronicDroneBuff>(); //The buff added to player after used the item
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			damage = item.damage;
			return true;
		}
	}
}
