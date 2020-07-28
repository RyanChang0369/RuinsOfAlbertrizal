using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Items
{
    
    public class Equiptment : Item
    {
        /// <summary>
        /// If true, then this item is already equipted on another slot.
        /// If true, then this item's buffs and StatGain will not be applied.
        /// </summary>
        public bool IsAClone { get; set; }

        /// <summary>
        /// Slots that this item can be equipted to.
        /// </summary>
        public List<SlotMode> EquiptableSlots { get; set; }

        /// <summary>
        /// When equipted, this item will take up all required slots.
        /// </summary>
        public List<SlotMode> RequiredSlots { get; set; }

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

        public BuffGuidStorage BuffImmunityStorage { get; set; }

        /// <summary>
        /// The character who equipts this equiptment is immune to the listed buffs. Permanent buffs are not affected.
        /// </summary>
        [XmlIgnore]
        public List<Buff> BuffImmunities { get; set; }

        public BuffGuidStorage GrantedBuffStorage { get; set; }

        /// <summary>
        /// The character who equipts this equiptment will receive the following permanent buffs.
        /// </summary>
        [XmlIgnore]
        public List<Buff> GrantedBuffs { get; set; }

        public List<Guid> AttackGuids { get; set; }

        [XmlIgnore]
        public List<Attack> Attacks { get; set; }

        public int ConnectionPointX { get; set; }

        public int ConnectionPointY { get; set; }

        public Point ConnectionPoint => new Point(ConnectionPointX, ConnectionPointY);

        [XmlIgnore]
        public Rectangle ConnectionArea => new Rectangle(ConnectionPointX, ConnectionPointY, 4, 4);

        public Equiptment()
        {
            StatGain = new int[GameBase.NumStats];
            BuffImmunities = new List<Buff>();
            Attacks = new List<Attack>();
            GrantedBuffStorage = new BuffGuidStorage();
            BuffImmunityStorage = new BuffGuidStorage();
            GrantedBuffs = new List<Buff>();
            EquiptableSlots = new List<SlotMode>();
            ConnectionPointX = 4;
            ConnectionPointY = 40;
        }

        public override void Load(Map map)
        {
            base.Load(map);

            GrantedBuffs = GrantedBuffStorage.Load(map.StoredBuffs);
            BuffImmunities = BuffImmunityStorage.Load(map.StoredBuffs);
            Attacks = map.StoredAttacks.FilterByGlobalID(AttackGuids);
        }

        public override void Unload(bool force)
        {
            base.Unload(force);

            if (force)
            {
                GrantedBuffStorage.Unload(GrantedBuffs);
                BuffImmunityStorage.Unload(BuffImmunities);
                AttackGuids = Attacks.ToGlobalIDList();
            }
        }

        public bool CanAttack()
        {
            foreach (SlotMode slot in EquiptableSlots)
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
