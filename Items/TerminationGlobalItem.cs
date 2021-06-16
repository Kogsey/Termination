using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Termination.Items
{
    public static class ExtendedItemRarity
    {
        public const int PrimeWeapon = -1000;
        public const int PostElectronicEye = 12;
        public const int PostElectronicEyeRare = -12;
        public const int PostUnknown = 13;
        public const int PostUnknownRare = -13;
    }
    public class TerminationGlobalItem : GlobalItem
    {
        public int extendedrarity;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine tooltipLine = ((IEnumerable<TooltipLine>)tooltips).FirstOrDefault<TooltipLine>((Func<TooltipLine, bool>)(x =>
            {
                if ((string)x.Name == "ItemName")
                    return (string)x.mod == "Terraria";
                return false;
            }));

            if (tooltipLine != null)
            {
                //Numbers begin at 12 and rise for post moonlord tiers
                //Negative numbers from post moonlord tiers are rare within the tier
                //"Prime Weapons" scale with boss kills

                switch (this.extendedrarity)
                {
                    case -1000:
                        tooltipLine.overrideColor = new Color?(new Color(128, Main.DiscoG, Main.DiscoB)); // The "Prime Weapons"
                        break;

                    case 12:
                        tooltipLine.overrideColor = new Color?(new Color(59, (int)byte.MaxValue, 211)); // ElectronicEye+
                        break;
                    case -12:
                        tooltipLine.overrideColor = new Color?(new Color(59, Main.DiscoG, 211)); // ElectronicEye+ negative
                        break;

                    case 13:
                        tooltipLine.overrideColor = new Color?(new Color((int)byte.MaxValue, 128, (int)byte.MaxValue)); // Undecided
                        break;
                    case -13:
                        tooltipLine.overrideColor = new Color?(new Color((int)byte.MaxValue, 128, Main.DiscoB)); // Undecided negative
                        break; 

                    case var expression when this.extendedrarity < 0:
                        break;

                        /*
                         * Calamity Code
                        case 12:
                            tooltipLine.overrideColor = new Color?(new Color(0, (int)byte.MaxValue, 200));
                            break;

                        case 13:
                            tooltipLine.overrideColor = new Color?(new Color(0, (int)byte.MaxValue, 0));
                            break;

                        case 14:
                            tooltipLine.overrideColor = new Color?(new Color(43, 96, 222));
                            break;

                        case 15:
                            tooltipLine.overrideColor = new Color?(new Color(108, 45, 199));
                            break;

                        case 16:
                            tooltipLine.overrideColor = new Color?(new Color((int)byte.MaxValue, 0, (int)byte.MaxValue));
                            break;

                        case 17:
                            break;

                        case 18:
                            tooltipLine.overrideColor = new Color?(new Color((int)Main.DiscoR, 100, (int)byte.MaxValue));
                            break;

                        case 19:
                            tooltipLine.overrideColor = new Color?(new Color(0, 0, (int)byte.MaxValue));
                            break;

                        case 20:
                            tooltipLine.overrideColor = new Color?(new Color((int)Main.DiscoR, (int)Main.DiscoG, (int)Main.DiscoB));
                            break;

                        case 21:
                            tooltipLine.overrideColor = new Color?(new Color(139, 0, 0));
                            break;

                        case 22:
                            tooltipLine.overrideColor = new Color?(new Color((int)byte.MaxValue, 140, 0));
                            break;
                        */
                }
            }
        }
    }
}