using Terraria;
using Terraria.ModLoader;
using Termination.NPCs;

namespace Termination.Buffs
{
	public class Suprised : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Suprised");
			Description.SetDefault("Increased movement speed"
                + "defenced halved");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.moveSpeed += 2f;
            player.maxRunSpeed += 2f;
            player.jumpSpeedBoost += 2f;
            player.statDefense = player.statDefense / 2;
        }
        
	}
}
