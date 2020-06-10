using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Items
{
    
    public class Consumable : Item, IRoundBasedObject
    {
        public List<Buff> Buffs { get; set; }

        public int[] StatGain { get; set; }

        [XmlIgnore]
        public int Duration
        {
            get
            {
                int duration = 0;

                foreach (Buff buff in Buffs)
                {
                    if (buff.LeveledDuration > duration)
                        duration = buff.LeveledDuration;
                }

                return duration;
            }
        }

        public int RoundsPassed { get; set; }

        [XmlIgnore]
        public bool HasEnded { get => RoundsPassed >= Duration; }

        public Consumable()
        {
            Buffs = new List<Buff>();
            StatGain = new int[GameBase.NumStats];
        }

        public void EndRound()
        {
            RoundsPassed++;
        }

        public void StartRound()
        {
            try
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].HasEnded)
                    {
                        Buffs.RemoveAt(i);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public int[] GetLifetimeStatGain(Character target)
        {
            int[] statGain = new int[GameBase.NumStats];
            foreach (Buff buff in Buffs)
                statGain = ArrayMethods.AddArrays(statGain, buff.GetLifetimeStatGain(target));

            return ArrayMethods.AddArrays(statGain, StatGain);
        }

        public int GetLifetimeStatGain(Character target, GameBase.Stats stat)
        {
            int total = StatGain[(int)stat];
            foreach (Buff buff in Buffs)
                total += buff.GetLifetimeStatGain(target, stat);

            return total;
        }

        /// <summary>
        /// The higher this number, the more "beneficial" this consumable is.
        /// </summary>
        /// <param name="user">The character using this consumable.</param>
        public int GetUtils(Character user)
        {
            int[] lifetimeStats = GetLifetimeStatGain(user);
            int utils = 0;

            foreach (int i in lifetimeStats)
                utils += i;

            return utils;
        }
    }
}
