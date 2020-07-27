using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs.Mechs
{
    public class SpringJump : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spring Jump");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.damage = 20;
            npc.defense = 9;
            npc.knockBackResist = 0.3f;
            npc.width = 62;
            npc.height = 46;
            animationType = 244;
            npc.aiStyle = 1;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit47;
            npc.DeathSound = SoundID.NPCDeath23;
            npc.value = Item.buyPrice(0, 0, 5, 15);
            banner = npc.type;
            bannerItem = mod.ItemType("SpringJumpBanner");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SpringJumpGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SpringJumpGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SpringJumpGore3"), 1f);
            }
        }

        public override void NPCLoot()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int centerX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int centerY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int halfLength = npc.width / 2 / 16 + 1;
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, 1, false, 0, false, false);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            return (Termination.NoZoneAllowWater(spawnInfo)) && !Main.dayTime && spawnInfo.spawnTileY < Main.worldSurface ? 0.1f : 0f;
        }

        public override void OnHitPlayer(Player player, int dmgDealt, bool crit)
        {
            int debuff = mod.BuffType("Nanobot");
            if (debuff >= 0)
            {
                player.AddBuff(debuff, 600, true);
            }
        }
    }
}