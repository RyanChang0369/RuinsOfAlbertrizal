using RuinsOfAlbertrizal.Characters;
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
        public int[] Slots { get; set; }

        public enum SlotEnum
        {
            Head, Torso, Legs, Feet, Neck, Hand1, Hand2, Finger1, Finger2, Finger3, Finger4, Finger5, Finger6, Finger7, Finger8, Finger9, Finger10
        }

        public int[] StatGain { get; set; }

        public Equiptment(string name, string description, int rarity,
            List<Enemy> droppedBy, int[] slots) : base(name, description, rarity, droppedBy)
        {
            Slots = slots;
        }
    }
}
