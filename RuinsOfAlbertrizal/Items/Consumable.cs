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
    [Serializable]
    public class Consumable : Item, IRoundBasedObject
    {
        public List<Buff> Buffs { get; set; }

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
        { }

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
    }
}
