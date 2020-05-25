﻿using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    public abstract class Character : WorldMapObject, ITurnBasedObject
    {
        public const int MaxTurns = 2;

        public int TurnsPassed { get; set; }

        /// <summary>
        /// The species name, such as human or slime
        /// </summary>
        public string GeneralName { get; set; }

        public int Level { get; set; }

        public enum ModelStyle
        { 
            [Description("Armor and weapons are shown. The world image should be a multiple of [x] by [y] pixels. See help page for more information.")]
            Humanoid,
            [Description("Helmet and weapons are shown. The world image should be a multiple of [x] by [y] pixels. See help page for more information.")]
            Slime,
            [Description("No armor or weapons are shown.")]
            Other
        }

        public ModelStyle StyleOfModel { get; set; }

        private AI.AIStyle aiStyle;

        public virtual AI.AIStyle AIStyle
        {
            get => aiStyle;
            set
            {
                if (value == AI.AIStyle.NoChange)
                    return;

                aiStyle = value;
            }
        }

        [XmlIgnore]
        public Hitbox Hitbox
        {
            get
            {
                try
                {
                    return new Hitbox(MapImage.Width, MapImage.Height);
                }
                catch (ArgumentNullException)
                {
                    return new Hitbox();
                }
            }
        }

        /// <summary>
        /// Image as it appears on the map. Determines hitbox size.
        /// </summary>
        public Bitmap MapImage { get; set; }

        /// <summary>
        /// The original stats that SHOULD NOT CHANGE.
        /// </summary>
        public int[] BaseStats { get; set; }

        [XmlIgnore]
        public int[] LeveledStats
        {
            get
            {
                int[] leveledStats = (int[])BaseStats.Clone();

                for (int i = 0; i < leveledStats.Length; i++)
                {
                    leveledStats[i] = (int)Math.Round(leveledStats[i] * (1 + (0.02 * (Level - 1))));
                }

                return leveledStats;
            }
        }

        /// <summary>
        /// Stats with the current armor equipted. Can be considered the max stats.
        /// </summary>
        [XmlIgnore]
        public int[] ArmoredStats
        {
            get
            {
                int[] armoredStats = { 0, 0, 0, 0, 0 };

                armoredStats = ArrayMethods.AddArrays(armoredStats, LeveledStats);

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    if (equiptment == null)
                        continue;

                    armoredStats = ArrayMethods.AddArrays(armoredStats, equiptment.StatGain);
                }

                return armoredStats;
            }
        }

        private int[] appliedStats = new int[5];

        /// <summary>
        /// Use this to apply damage
        /// </summary>
        public int[] AppliedStats //ErrorMap 1
        {
            get => appliedStats;
            set
            {
                //Do not allow healing if current map is null or if character is dead
                if (!GameBase.Initialized() || IsDead)
                    return;

                value = CapAppliedStats(value);
                appliedStats = value;

                CheckIfDead();
            }
        }

        /// <summary>
        /// Caps the given 5 element array to the leveled stats.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int[] CapAppliedStats(int[] arr)
        {
            for (int i = 0; i < 5; i++)
            {
                if (arr[i] > LeveledStats[i])
                    arr[i] = LeveledStats[i];
                else if (arr[i] < 0)
                    arr[i] = 0;
            }

            return arr;
        }

        /// <summary>
        /// The stats from armor and buffs
        /// </summary>
        [XmlIgnore]
        public int[] ArmorAndBuffStats
        {
            get
            {
                int[] armorAndBuffStats = new int[5];

                armorAndBuffStats = ArrayMethods.AddArrays(armorAndBuffStats, ArmoredStats);

                foreach (Buff buff in CurrentBuffs)
                {
                    if (buff == null)
                        continue;

                    armorAndBuffStats = ArrayMethods.AddArrays(armorAndBuffStats, buff.LeveledStatGain);
                }

                return armorAndBuffStats;
            }
        }

        [XmlIgnore]
        public int[] CurrentStats
        {
            get
            {
                int[] currentStats = { 0, 0, 0, 0, 0 };

                currentStats = ArrayMethods.AddArrays(currentStats, ArmorAndBuffStats);
                currentStats = ArrayMethods.AddArrays(currentStats, AppliedStats);

                return currentStats;
            }
        }

        /// <summary>
        /// This character will recieve the following buffs.
        /// </summary>
        public List<Buff> PermanentBuffs { get; set; }

        /// <summary>
        /// This character is immune to the following buffs of the same name. Permanent buffs are ignored.
        /// </summary>
        public List<Buff> BuffImmunities { get; set; }

        private List<Buff> appliedBuffs = new List<Buff>();

        /// <summary>
        /// Use this to directly apply buffs. These buffs will expire.
        /// </summary>
        public List<Buff> AppliedBuffs
        {
            get => appliedBuffs;
            set
            {
                if (!GameBase.Initialized())
                    return;

                //Do not allow buffs to be added if character is dead.
                if (!IsDead)
                    appliedBuffs = value;
                else
                {
                    Die();
                    return;
                }
            }
        }

        [XmlIgnore]
        public List<Buff> CurrentBuffs
        {
            get
            {
                List<Buff> currentBuffs = new List<Buff>();
                foreach (Buff buff in AppliedBuffs)
                {
                    currentBuffs.Add(buff);
                }

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    if (equiptment == null)
                        continue;

                    foreach (Buff buff in equiptment.BuffImmunities)
                    {
                        currentBuffs.Add(buff);
                    }
                }

                foreach (Consumable consumable in CurrentConsumables)
                {
                    foreach (Buff buff in consumable.Buffs)
                    {
                        currentBuffs.Add(buff);
                    }
                }

                foreach (Buff buff in BuffImmunities)
                {
                    currentBuffs.RemoveAll(item => item.Name == buff.Name);
                }

                foreach (Buff buff in PermanentBuffs)
                {
                    currentBuffs.Add(buff);
                }

                return currentBuffs;
            }
        }

        [XmlIgnore]
        /// <summary>
        /// How strong the character is. Ignores any non-static buffs.
        /// </summary>
        public int BattleIndex
        {
            get
            {
                if (this == null)
                    return 0;

                int total = 0;

                foreach (int i in ArmoredStats)
                {
                    total += i;
                }

                return total;
            }
        }

        /// <summary>
        /// The equiptment the character has equipted
        /// </summary>
        public Equiptment[] CurrentEquiptments { get; set; }

        /// <summary>
        /// The consumables the character has consumed
        /// </summary>
        public List<Consumable> CurrentConsumables { get; set; }

        public List<Equiptment> InventoryEquiptments { get; set; }

        public List<Consumable> InventoryConsumables { get; set; }

        public List<Item> InventoryItems { get; set; }

        public List<Attack> Attacks { get; set; }

        /// <summary>
        /// True if character is charging an attack, false otherwise.
        /// </summary>
        public bool IsCharging { get; set; }

        [XmlIgnore]
        public bool IsDead { get => CurrentStats[0] <= 0; }

        public Character()
        {
            Level = 1;
            CurrentEquiptments = new Equiptment[16];        //16 possible slots for equiptment
            InventoryEquiptments = new List<Equiptment>();
            CurrentConsumables = new List<Consumable>();
            InventoryConsumables = new List<Consumable>();
            InventoryItems = new List<Item>();
            Attacks = new List<Attack>();
            BaseStats = new int[5];
            PermanentBuffs = new List<Buff>();
        }

        /// <summary>
        /// Equipts an equiptable
        /// </summary>
        /// <param name="index">The index of the item in InventoryEquiptments</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Equipt(int index)
        {
            Equiptment equiptment = InventoryEquiptments[index];
            Unequipt(index);

            foreach (Equiptment.SlotMode slotMode in equiptment.Slots)
            {

                CurrentEquiptments[(int)slotMode] = equiptment;
            }
        }

        /// <summary>
        /// Removes this equiptment and any of the slots it may have occupied.
        /// </summary>
        /// <param name="index"></param>
        public void Unequipt(int index)
        {
            foreach (Equiptment.SlotMode slotMode in CurrentEquiptments[index].Slots)
            {
                CurrentEquiptments[(int)slotMode] = null;
            }
            InventoryEquiptments.Add(CurrentEquiptments[index]);
        }

        /// <summary>
        /// Consumes an consumable
        /// </summary>
        /// <param name="index">The index of the item in InventoryConsumables</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Consume(int index)
        {
            Consumable consumable = InventoryConsumables[index];
            InventoryConsumables.RemoveAt(index);
            CurrentConsumables.Add(consumable);
        }

        public void Attack(int attackIndex, Character character)
        {
            Attacks[attackIndex].BeginAttack(character);
        }

        public void RecoverMana()
        {
            AppliedStats[1] += (int)Math.Round(LeveledStats[1] * 0.3);
            EndTurn();
        }

        public abstract void Die();

        public void EndTurn()
        {
            CheckIfDead();
        }

        public void StartTurn()
        {
            CheckIfDead();
        }

        public void StartRound()
        {
            //Remove all expired consumables and buffs
            CurrentConsumables.RemoveAll(item => item.HasEnded);
            AppliedBuffs.RemoveAll(item => item.HasEnded);
        }
        
        public void EndRound()
        {

        }

        public void CheckIfDead()
        {
            if (IsDead)
                Die();
        }

        public void GetAttacked(Attack attack)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                if (buff.TypeOfBuff == Buff.BuffType.LastHope && CurrentStats[0] != 1
                    && (CurrentStats[0] - attack.StatLoss[0]) <= 0)
                {
                    AppliedStats[0] = (-1 * CurrentStats[0]) + 1;
                    return;
                }
                else if (buff.TypeOfBuff == Buff.BuffType.Invunerable)
                    return;
            }
            

            for (int i = 0; i < attack.StatLoss.Length; i++)
            {
                AppliedStats[i] -= attack.StatLoss[i];
            }
        }
    }
}
