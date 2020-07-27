using Termination.Items;
using Terraria;

namespace Termination
{
    public static class TerminationUtils
    {
        public static TerminationGlobalItem Termination(this Item item)
        {
            return (TerminationGlobalItem)item.GetGlobalItem<TerminationGlobalItem>();
        }
    }
}