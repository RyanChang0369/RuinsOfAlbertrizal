using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Hazard : ObjectOfAlbertrizal
    {
        public int[] StatLoss { get; set; }

        public List<Buff> Buffs { get; set; }

        public enum DamageDirection
        {
            Omnidirectional,
            Top,
            Bottom,
            Left,
            Right
        }

        public enum HazardType
        {
            /// <summary>
            /// Characters appear behind this hazard and can stand on it. 
            /// </summary>
            Block,

            Wall
        }
    }
}
