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
            None,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Moves towards player if no attacks are in range.
            /// No regard to health.
            /// </summary>
            Beserk,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Priotizes movement towards player. Moves before attacking.
            /// No regard to health.
            /// </summary>
            Beserk_Melee,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Priotizes movement away from player. Moves before attacking.
            /// No regard to health.
            /// </summary>
            Beserk_Ranged
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
