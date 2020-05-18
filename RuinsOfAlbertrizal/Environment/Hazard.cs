using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Hazard : Block
    {
        public int[] StatLoss { get; set; }

        public List<Buff> Buffs { get; set; }

        public DamageDirection DirectionOfDamage { get; set; }

        public Hazard()
        {
            StatLoss = new int[5];
            Buffs = new List<Buff>();
            DirectionOfDamage = new DamageDirection();
            TypeOfBlock = new BlockType();
        }
    }
}
