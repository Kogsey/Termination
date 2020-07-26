using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termination.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
