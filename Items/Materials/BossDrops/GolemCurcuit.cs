using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Materials.BossDrops
{
    public class GolemCurcuit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golem Primary Processor Unit");
            Tooltip.SetDefault("How did the Lizhards managed to get hold of technology as advanced as this?");
        }

        public override void SetDefaults()
        {
            item.width = 13;
            item.height = 13;
            item.maxStack = 999;
            item.value = 53200;
            item.rare = ItemRarityID.Lime;
        }
    }
}