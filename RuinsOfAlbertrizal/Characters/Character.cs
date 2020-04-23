using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Characters
{
    public abstract class Character : ITurnBasedObject
    {
        /// <summary>
        /// The name of the "species" such as human or orc
        /// </summary>
        public string GeneralName { get; set; }

        /// <summary>
        /// The proper name, such as Bob or Robert
        /// </summary>
        public string SpecificName { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public int[] BaseStats { get; set; }

        [XmlIgnore]
        public int[] CurrentStats {
            get
            {
                int[] currentStats = { 0, 0, 0, 0, 0 };

                List<Buff> currentBuffs = CurrentBuffs;

                foreach (Buff buff in currentBuffs)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, buff.StatGain);
                }

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, equiptment.StatGain);
                }

                return currentStats;
            }
        }

        public List<int> Abilities { get; set; }

        public List<Buff> AppliedBuffs { get; set; }

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
                    foreach (Buff buff in equiptment.Buffs)
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

        /// <summary>
        /// The equiptment the character has equipted
        /// </summary>
        public List<Equiptment> CurrentEquiptments { get; set; }
        
        /// <summary>
        /// The consumables the character has consumed
        /// </summary>
        public List<Consumable> CurrentConsumables { get; set; }

        public List<Equiptment> InventoryEquiptments { get; set; }

        public List<Consumable> InventoryConsumables { get; set; }

        public List<Item> InventoryItems { get; set; }

        [XmlIgnore]
        public bool IsDead { get => CurrentStats[0] <= 0; }

        public Character()
        {
            BaseStats = new int[5];
        }

        /// <summary>
        /// Equipts an equiptable
        /// </summary>
        /// <param name="index">The index of the item in InventoryEquiptments</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Equipt(int index)
        {
            Equiptment equiptment = InventoryEquiptments[index];
            
            foreach(Equiptment equiptment1 in CurrentEquiptments)
            {
                foreach (Equiptment.SlotMode slot in equiptment1.Slots)
                {
                    if (equiptment.Slots.Contains(slot))
                    {
                        CurrentEquiptments.Remove(equiptment1);
                    }
                }
            }

            InventoryEquiptments.RemoveAt(index);

            CurrentEquiptments.Add(equiptment);
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

        public void TakeDamage(int dmg)
        {
            CurrentStats[(int)GameBase.Stats.HP] -= dmg;
        }

        public void Die()
        {
            
        }

        public void TurnEnded()
        {
            if (IsDead)
                Die();
        }

        public void TurnStarted()
        {
            if (IsDead)
                Die();

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
