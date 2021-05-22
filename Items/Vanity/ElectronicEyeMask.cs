using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class ElectronicEyeMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}