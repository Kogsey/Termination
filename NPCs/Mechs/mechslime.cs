using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.NPCs.Mechs
{
    public class mechslime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("mechslime");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 350;
            npc.damage = 100;
            npc.defense = 12;
            npc.knockBackResist = 0.5f;
            npc.width = 40;
            npc.height = 40;
            animationType = 121;
            npc.aiStyle = 14;
            npc.noGravity = true;
            npc.npcSlots = 1f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 10, 0);
            banner = npc.type;
            bannerItem = mod.ItemType("mechslimeBanner");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y - 48, mod.NPCType("SpringJump"));
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