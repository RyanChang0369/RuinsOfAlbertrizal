using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Items
{
    public class Equiptment : Item
    {
        /// <summary>
        /// Refer to SlotEnum
        /// </summary>
        public SlotMode[] Slots { get; set; }

        public enum SlotMode
        {
            Head, Torso, Legs, Feet, Neck, Hand1, Hand2, Finger1, Finger2, Finger3, Finger4, Finger5, Finger6, Finger7, Finger8, Finger9, Finger10
        }

        public int[] StatGain { get; set; }

        public List<Buff> Buffs { get; set; }

        public Equiptment()
        { }
    }
}
