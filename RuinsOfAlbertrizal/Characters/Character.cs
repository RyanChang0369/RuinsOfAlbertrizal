using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    public abstract class Character : WorldMapObject, ITurnBasedObject
    {
        /// <summary>
        /// The species name, such as human or slime
        /// </summary>
        public string GeneralName { get; set; }

        public int Level { get; set; }

        public AI.AIStyle AIStyle { get; set; }

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
        /// Image as it appears on the map. Determies hitbox size.
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
        /// Stats with te current armor equipted
        /// </summary>
        [XmlIgnore]
        public int[] ArmoredStats
        {
            get
            {
                int[] currentStats = { 0, 0, 0, 0, 0 };

                currentStats = ArrayMethods.AddArrays(currentStats, LeveledStats);

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, equiptment.StatGain);
                }

                return currentStats;
            }
        }

        /// <summary>
        /// Use this to apply damage
        /// </summary>
        public int[] AppliedStats { get; set; }

        [XmlIgnore]
        public int[] CurrentStats {
            get
            {
                int[] currentStats = { 0, 0, 0, 0, 0 };

                currentStats = ArrayMethods.AddArrays(currentStats, ArmoredStats);

                foreach (Buff buff in CurrentBuffs)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, buff.LeveledStatGain);
                }

                currentStats = ArrayMethods.AddArrays(currentStats, AppliedStats);

                return currentStats;
            }
        }

        //public List<int> Abilities { get; set; }

        public List<Buff> AppliedBuffs { get; set; }

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

                return currentBuffs;
            }
        }

        [XmlIgnore]
        /// <summary>
        /// How strong the character is. Ignores any buffs.
        /// </summary>
        public int BattlePoints
        {
            get
            {
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
            BaseStats = new int[5];
            AppliedStats = new int[5];
            AppliedBuffs = new List<Buff>();
            CurrentEquiptments = new Equiptment[16];        //16 possible slots for equiptment
            InventoryEquiptments = new List<Equiptment>();
            CurrentConsumables = new List<Consumable>();
            InventoryConsumables = new List<Consumable>();
            InventoryItems = new List<Item>();
            Attacks = new List<Attack>();
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

        public abstract void Die();

        public void TurnEnded()
        {
            if (IsDead)
                Die();
        }

        public void TurnStarted()
        {
            if (IsDead)
                Die();

            //Remove any expired consumables
            try
            {
                for (int i = 0; i < CurrentConsumables.Count; i++)
                {
                    if (CurrentConsumables[i].HasEnded)
                    {
                        CurrentConsumables.RemoveAt(i);
                    }    
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
