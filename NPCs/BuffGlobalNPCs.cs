using Terraria;
using Terraria.ModLoader;

namespace Termination.NPCs
{
    internal class BuffGlobalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool Nanobot = false;

        public override void ResetEffects(NPC npc)
        {
            Nanobot = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Nanobot)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 16;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
        }
    }
}