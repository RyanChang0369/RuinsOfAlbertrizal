using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.AIs
{
    public class AI
    {
        /// <summary>
        /// Determines how the AI acts
        /// </summary>
        public AIStyle Style { get; set; }

        public enum AIStyle
        {
            /// <summary>
            /// The player controls this character. Only works with the player character, and is treated as None everywhere else
            /// </summary>
            Player = -1,
            None = 0,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Moves so that its most damaging attack or attacks is in range of the player.
            /// Attacks twice if doing so will deal the most damage to the player.
            /// No regard to health.
            /// Recovers only if unable to attack.
            /// Cannot use items.
            /// </summary>
            Beserk = 10,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Moves so that its most damaging attack or attacks is in range of the player.
            /// Attacks twice if doing so will deal the most damage to the player.
            /// Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack.
            /// Recovers only if unable to attack.
            /// Uses items to recover health.
            /// </summary>
            Beserk_UseItem = 11,
            /// <summary>
            /// Tries to stay above the player.
            /// Attacks player with the most damaging attacks possible.
            /// If has attacked, move so it is out of range of player.
            /// Attacks twice only if player is very far away (twice the range of the player).
            /// Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack. Only heals if out of range of player.
            /// Recovers when unable to attack and player is out of range or when player is very far away (twice the range of the player).
            /// Uses items when unable to attack and player is out of range or when player is very far away (twice the range of the player).
            /// </summary>
            Flying = 20,
        }

        public static string[] AINames
        {
            get
            {
                return Enum.GetNames(typeof(AIStyle));
            }
        }

        public static string[] AIStyleTooltips =
        {
            "",

            "Attacks player with the most damaging attacks possible. " +
            "Moves towards player if no attacks are in range. " +
            "No regard to health.",

            "Attacks player with the most damaging attacks possible. " +
            "Priotizes movement towards player. Moves before attacking. " +
            "No regard to health.",

            "Attacks player with the most damaging attacks possible. " +
            "Priotizes movement away from player. Moves before attacking. " +
            "No regard to health."
        };
    }
}
