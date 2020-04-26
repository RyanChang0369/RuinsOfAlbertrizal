using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static RuinsOfAlbertrizal.AIs.AI;

namespace RuinsOfAlbertrizal.Mechanics
{
    /// <summary>
    /// Buffs and debuffs
    /// </summary>
    public class Buff : ObjectOfAlbertrizal, ITurnBasedObject
    {
        /// <summary>
        /// How many turns will this last for?
        /// </summary>
        public int Duration { get; set; }

        public int TurnsPassed { get; set; }

        [XmlIgnore]
        public bool HasEnded { get => TurnsPassed >= Duration; }

        /// <summary>
        /// See GameBase.Stats for values
        /// </summary>
        public int[] StatGain { get; set; }

        ///// <summary>
        ///// Changes the AI
        ///// </summary>
        public AIStyle AIChange { get; set; }

        public Buff()
        { }

        public void TurnEnded()
        {
            TurnsPassed++;
        }

        public void TurnStarted()
        {
            
        }
    }
}
