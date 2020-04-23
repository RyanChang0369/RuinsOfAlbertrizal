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
    public class Consumable : Item, ITurnBasedObject
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
                    if (buff.Duration > duration)
                        duration = buff.Duration;
                }

                return duration;
            }
        }

        public int TurnsPassed { get; set; }

        [XmlIgnore]
        public bool HasEnded { get => TurnsPassed >= Duration; }

        public Consumable()
        { }

        public void TurnEnded()
        {
            TurnsPassed++;
        }

        public void TurnStarted()
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
