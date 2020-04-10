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
        /// The amount of percentage one gains from killing one enemy.
        /// Boss fight starts at next encounter if this is equal to or exceeds 100%.
        /// </summary>
        public double PointGainPerKill { get; set; }

        protected Enemy()
        {

        }

        public Enemy(string generalName, string specificName, string description, int[] baseValues)
            : base(generalName, specificName, description, baseValues)
        {

        }

        public Enemy(string generalName, string specificName, string description,
            int[] baseValues, double pointGainPerKill) :
             base(generalName, specificName, description, baseValues)
        {
            PointGainPerKill = pointGainPerKill;
        }
    }
}
