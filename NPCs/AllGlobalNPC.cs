using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs
{
    class AllGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GolemCurcuit"), 5 + Main.rand.Next(3));
            }
            // Addtional if statements here if you would like to add drops to other vanilla npc.
            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("NatureGift"), 15 + Main.rand.Next(5));
            }
    
            if (npc.type == NPCID.KingSlime)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SuperSlime"), 5 + Main.rand.Next(10));
            }
    
            if (npc.type == NPCID.EyeofCthulhu)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Cthulhustooth"), 6);
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MLMagic"), 20);
            }
        }
    }
}