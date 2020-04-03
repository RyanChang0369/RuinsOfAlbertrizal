using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    class Hazard
    {
        public string Name { get; set; }

        public List<Buff> Buffs { get; set; }

        public Hazard(string name, List<Buff> buffs)
        {
            Name = name;
            Buffs = buffs;
        }
    }
}
