using RuinsOfAlbertrizal.AIs;
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

        public Enemy()
        {
            AIStyle = AIs.AI.AIStyle.NoAI;
        }

        public override void Die()
        {
            throw new NotImplementedException("");
        }
    }
}
