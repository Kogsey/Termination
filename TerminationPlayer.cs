using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace Termination
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers and additions to knockback and crit.
    public class TerminationPlayer : ModPlayer
    {

        public bool Nanobot = false;

        public bool HardenedLuminiteSetBonus = false;

        public static TerminationPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<TerminationPlayer>();
        }

        // Vanilla only really has damage multipliers in code
        // And crit and knockback is usually just added to
        // As a modder, you could make separate variables for multipliers and simple addition bonuses

        public override void ResetEffects()
        {
            ResetVariables();
            Nanobot = false;

            HardenedLuminiteSetBonus = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
            Nanobot = false;

            HardenedLuminiteSetBonus = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Nanobot)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 16;
            }
        }

        private void ResetVariables()
        {
        }
    }
}
