using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items
{
    public class ElectronicEyeBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Yellow;
            item.expert = true;
        }

        public override int BossBagNPC => mod.NPCType("ElectronicEye");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.QuickSpawnItem(mod.ItemType("BallOMetal"));
            switch (Main.rand.Next(4))
            {
                case 0:
                    _ = mod.ItemType("NanabotSwarm");
                    break;

                case 1:
                    _ = mod.ItemType("OrbBoomerang");
                    break;

                case 2:
                    _ = mod.ItemType("ElectronicEyeMount");
                    break;

                case 3:
                    _ = mod.ItemType("reprogrammedBoss1");
                    break;
            }
        }
    }
}