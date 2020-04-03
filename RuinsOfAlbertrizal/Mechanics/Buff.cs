using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    /// <summary>
    /// Buffs and debuffs
    /// </summary>
    public class Buff
    {
        public string Name { get; set; }
        public string Description { get; set; }

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

        /// <summary>
        /// If true, then player cannot do anything.
        /// </summary>
        public bool Immobalized { get; set; }

        /// <summary>
        /// Creates a new buff
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="statGain"></param>
        /// <param name="immobalized"></param>
        public Buff(string name, string description, int[] statGain, bool immobalized)
        {
            Name = name;
            Description = description;
            StatGain = statGain;
            Immobalized = immobalized;
        }

        /// <summary>
        /// Creates a new non-immobalizing buff
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="statGain"></param>
        public Buff(string name, string description, int[] statGain) : this(name, description, statGain, false)
        {   }

        //public Buff(string name, string description, int duration, int interval,
        //    int HPGainPerInterval, int manaGainPerInterval, int defGainPerInterval,
        //    int dmgGainPerInterval, int spdGainPerInterval, double jumpGainPerInterval)
        //{
        //    Name = name;
        //    Description = description;

        //    Duration = duration;
        //    Interval = interval;

        //    this.HPGainPerInterval = HPGainPerInterval;
        //    DefGainPerInterval = defGainPerInterval;
        //    DmgGainPerInterval = dmgGainPerInterval;
        //    SpdGainPerInterval = spdGainPerInterval;
        //    JumpGainPerInterval = jumpGainPerInterval;
        //}

        //public Buff(string name, string description, int duration, int interval,
        //    int HPGain, int manaGain, int defGain, int dmgGain, int spdGain, double jumpGain,
        //    int HPGainPerInterval, int manaGainPerInterval, int defGainPerInterval,
        //    int dmgGainPerInterval, int spdGainPerInterval, double jumpGainPerInterval)
        //{
        //    Name = name;
        //    Description = description;

        //    Duration = duration;
        //    Interval = interval;

        //    this.HPGain = HPGain;
        //    DefGain = defGain;
        //    DmgGain = dmgGain;
        //    SpdGain = spdGain;
        //    JumpGain = jumpGain;

        //    this.HPGainPerInterval = HPGainPerInterval;
        //    DefGainPerInterval = defGainPerInterval;
        //    DmgGainPerInterval = dmgGainPerInterval;
        //    SpdGainPerInterval = spdGainPerInterval;
        //    JumpGainPerInterval = jumpGainPerInterval;
        //}
    }
}
