using Termination.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Buffs
{
    public class Nanobot : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("NanoBot infection!");
            Description.SetDefault("losing life"
                + "can't regenerate health");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerminationPlayer>().Nanobot = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BuffGlobalNPCs>().Nanobot = true;
        }
    }
}