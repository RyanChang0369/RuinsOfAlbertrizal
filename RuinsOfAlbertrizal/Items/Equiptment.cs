using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

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
            [Description("No Slot")]
            None,
            [Description("Head")]
            Head,
            [Description("Neck")]
            Neck,
            [Description("Torso")]
            Torso,
            [Description("Left Hand")]
            Hand1,
            [Description("Right Hand")]
            Hand2,
            [Description("Legs")]
            Legs,
            [Description("Feet")]
            Feet,
            [Description("Accessory 1")]
            Finger1,
            [Description("Accessory 2")]
            Finger2,
            [Description("Accessory 3")]
            Finger3,
            [Description("Accessory 4")]
            Finger4,
            [Description("Accessory 5")]
            Finger5,
            [Description("Accessory 6")]
            Finger6,
            [Description("Accessory 7")]
            Finger7,
            [Description("Accessory 8")]
            Finger8,
            [Description("Accessory 9")]
            Finger9,
            [Description("Accessory 10")]
            Finger10
        }

        public int[] StatGain { get; set; }

        /// <summary>
        /// The character who equipts this equiptment is immune to the listed buffs. Permanent buffs are not affected.
        /// </summary>
        public List<Buff> BuffImmunities { get; set; }

        /// <summary>
        /// The character who equipts this equiptment will receive the following permanent buffs.
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

        public static List<Equiptment> GetAttackableEquiptment(List<Equiptment> allEquiptments)
        {
            List<Equiptment> equiptments = new List<Equiptment>();
            
            foreach (Equiptment equiptment in allEquiptments)
            {
                if (equiptment != null && equiptment.CanAttack())
                    equiptments.Add(equiptment);
            }

            return equiptments;
        }
    }
}
