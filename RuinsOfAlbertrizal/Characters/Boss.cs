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
        public string[] BossMessage { get; set; }

        private Boss()
        {

        }

        public Boss(string generalName, string specificName, int[] baseValues, string[] bossMessage) : base(generalName, specificName, baseValues)
        {
        }

        public void SummonBoss()
        {
            foreach (string line in BossMessage)
            {
                
            }
        }
    }
}
