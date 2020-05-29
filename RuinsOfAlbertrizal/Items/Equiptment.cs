using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;

namespace RuinsOfAlbertrizal.Items
{
    public class Equiptment : Item
    {
        /// <summary>
        /// Refer to SlotEnum
        /// </summary>
        public List<SlotMode> Slots { get; set; }

        public enum SlotMode
        {
            None, Head, Neck, Torso, Hand1, Hand2, Legs, Feet, Finger1, Finger2, Finger3, Finger4, Finger5, Finger6, Finger7, Finger8, Finger9, Finger10
        }

        public int[] StatGain { get; set; }

        /// <summary>
        /// The character who equipts this equiptment is immune to the listed buffs. Permanent buffs are not affected.
        /// </summary>
        public List<Buff> BuffImmunities { get; set; }

        /// <summary>
        /// The character who equipts this equiptment will recieve the following permanent buffs.
        /// </summary>
        public List<Buff> GrantedBuffs { get; set; }

        public List<Attack> Attacks { get; set; }

        public Equiptment()
        {
            StatGain = new int[GameBase.NumStats];
            BuffImmunities = new List<Buff>();
            Attacks = new List<Attack>();
            Slots = new List<SlotMode>();
        }

        public bool CanAttack()
        {
            foreach (SlotMode slot in Slots)
            {
                if (slot == SlotMode.Hand1 || slot == SlotMode.Hand2)
                    return true;
            }

            return false;
        }

        public void Attack()
        {
            if (!CanAttack())
                return;

            throw new NotImplementedException();
        }
    }
}
