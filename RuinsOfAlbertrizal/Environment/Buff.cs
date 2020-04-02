using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
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

        /// <summary>
        /// How many turns will pass before this does effect?
        /// </summary>
        public int Interval { get; set; }

        public int HPGainPerInterval { get; set; }
        public int ManaGainPerInterval { get; set; }
        public int DefGainPerInterval { get; set; }
        public int DmgGainPerInterval { get; set; }
        public int SpdGainPerInterval { get; set; }
        public double JumpGainPerInterval { get; set; }

        public int HPGain { get; set; }
        public int ManaGain { get; set; }
        public int DefGain { get; set; }
        public int DmgGain { get; set; }
        public int SpdGain { get; set; }
        public double JumpGain { get; set; }

        public Buff(string name, string description,
            int HPGain, int manaGain, int defGain, int dmgGain, int spdGain, double jumpGain)
        {
            Name = name;
            Description = description;
            this.HPGain = HPGain;
            DefGain = defGain;
            DmgGain = dmgGain;
            SpdGain = spdGain;
            JumpGain = jumpGain;
        }

        public Buff(string name, string description, int duration, int interval,
            int HPGainPerInterval, int manaGainPerInterval, int defGainPerInterval,
            int dmgGainPerInterval, int spdGainPerInterval, double jumpGainPerInterval)
        {
            Name = name;
            Description = description;

            Duration = duration;
            Interval = interval;

            this.HPGainPerInterval = HPGainPerInterval;
            DefGainPerInterval = defGainPerInterval;
            DmgGainPerInterval = dmgGainPerInterval;
            SpdGainPerInterval = spdGainPerInterval;
            JumpGainPerInterval = jumpGainPerInterval;
        }

        public Buff(string name, string description, int duration, int interval,
            int HPGain, int manaGain, int defGain, int dmgGain, int spdGain, double jumpGain,
            int HPGainPerInterval, int manaGainPerInterval, int defGainPerInterval,
            int dmgGainPerInterval, int spdGainPerInterval, double jumpGainPerInterval)
        {
            Name = name;
            Description = description;

            Duration = duration;
            Interval = interval;

            this.HPGain = HPGain;
            DefGain = defGain;
            DmgGain = dmgGain;
            SpdGain = spdGain;
            JumpGain = jumpGain;

            this.HPGainPerInterval = HPGainPerInterval;
            DefGainPerInterval = defGainPerInterval;
            DmgGainPerInterval = dmgGainPerInterval;
            SpdGainPerInterval = spdGainPerInterval;
            JumpGainPerInterval = jumpGainPerInterval;
        }
    }
}
