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
            /// Moves towards player if no attacks are in range.
            /// No regard to health.
            /// </summary>
            Beserk = 1,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Priotizes movement towards player. Moves before attacking.
            /// No regard to health.
            /// </summary>
            Beserk_Melee = 2,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Priotizes movement away from player. Moves before attacking.
            /// No regard to health.
            /// </summary>
            Beserk_Ranged = 3
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
