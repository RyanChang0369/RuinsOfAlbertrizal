using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Characters
{
    public class Boss : Enemy
    {
        /// <summary>
        /// Message to play when boss encountered
        /// </summary>
        public string[] BossMessageStart { get; set; }

        /// <summary>
        /// Message to play when boss is defeated
        /// </summary>
        public string[] BossMessageDefeat { get; set; }

        /// <summary>
        /// Message to play when player is defeated
        /// </summary>
        public string[] BossMessageVictory { get; set; }

        public Boss()
        {

        }

        //public Boss(string generalName, string specificName, string description, int[] baseValues,
        //    string[] bossMessageStart, string[] bossMessageDefeat, string[] bossMessageVictory)
        //    : base(generalName, specificName, description, baseValues)
        //{
        //    BossMessageStart = bossMessageStart;
        //    BossMessageDefeat = bossMessageDefeat;
        //    BossMessageVictory = bossMessageVictory;
        //}

        public void SummonBoss()
        {
            
        }
    }
}
