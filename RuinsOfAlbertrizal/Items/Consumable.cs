using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Items
{
    public class Consumable : Item
    {
        public List<Buff> Buffs { get; set; }

        private Consumable()
        { }

        public Consumable(string name, string description, int rarity,
            List<Enemy> droppedBy, List<Buff> buffs) : base(name, description, rarity, droppedBy)
        {
            Buffs = buffs;
        }
    }
}
