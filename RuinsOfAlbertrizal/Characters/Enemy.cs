using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Characters
{
    public class Enemy : Character
    {
        /// <summary>
        /// Determines how the AI acts
        /// </summary>
        public AIStyle AI { get; set; }

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

        /// <summary>
        /// The amount of percentage one gains from killing one enemy.
        /// Boss fight starts at next encounter if this is equal to or exceeds 100%.
        /// </summary>
        public double PointGainPerKill { get; set; }

        public Enemy()
        {

        }

        //public Enemy(string generalName, string specificName, string description, int[] baseValues)
        //    : base(generalName, specificName, description, baseValues)
        //{

        //}

        //public Enemy(string generalName, string specificName, string description,
        //    int[] baseValues, double pointGainPerKill) :
        //     base(generalName, specificName, description, baseValues)
        //{
        //    PointGainPerKill = pointGainPerKill;
        //}
    }
}
