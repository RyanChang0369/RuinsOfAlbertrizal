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
    public abstract class Character
    {
        public int AIStyle { get; set; }

        /// <summary>
        /// The name of the "species" such as human or orc
        /// </summary>
        public string GeneralName { get; set; }

        /// <summary>
        /// The proper name, such as Bob or Robert
        /// </summary>
        public string SpecificName { get; set; }

        public string Description { get; set; }

        public int[] BaseStats { get; set; }

        [XmlIgnore]
        public int[] CurrentStats {
            get
            {
                int[] currentStats = { 0, 0, 0, 0, 0 };
                foreach (Buff buff in Buffs)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, buff.StatGain);
                }

                foreach (Equiptment equiptment in Equiptments)
                {
                    currentStats = ArrayMethods.AddArrays(currentStats, equiptment.StatGain);
                }

                return currentStats;
            }
        }

        public List<int> Abilities { get; set; }

        public List<Buff> Buffs { get; set; }

        public List<Equiptment> Equiptments { get; set; }

        [XmlIgnore]
        public bool IsDead { get => CurrentStats[0] <= 0; }

        protected Character()
        {

        }

        /// <summary>
        /// Creates a new charecter with the following values, abilities, and buffs
        /// </summary>
        /// <param name="generalName">The name of the "species" such as human or orc</param>
        /// <param name="specificName">The proper name, such as Bob or Robert</param>
        /// <param name="currentStats">[0]=HP, [1]=Mana, [2]=Def, [3]=Spd, [4]=Jump</param>
        /// <param name="abilities">See Ability class</param>
        public Character(string generalName, string specificName, string description, int[] baseStats,
            List<int> abilities, List<Buff> buffs, List<Equiptment> equiptments)
        {
            GeneralName = generalName;
            SpecificName = specificName;
            Description = description;
            BaseStats = baseStats;

            Abilities = abilities;
            Buffs = buffs;
            Equiptments = equiptments;
        }

        /// <summary>
        /// Creates a new charecter with the following values and no abilities
        /// </summary>
        /// <param name="generalName">The name of the "species" such as human or orc</param>
        /// <param name="specificName">The proper name, such as Bob or Robert</param>
        public Character(string generalName, string specificName, string description, int[] baseStats)
            : this(generalName, specificName, description, baseStats, new List<int>(), new List<Buff>(), new List<Equiptment>())
        {
            
        }

        public void AddBuff(Buff buff)
        {
            Buffs.Add(buff);
        }

        public void TakeDamage(int dmg)
        {
            CurrentStats[(int)GameBase.Stats.HP] -= dmg;
        }

        public void Die()
        {
            MessageBox.Show("You have died.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            throw new Exception("Git gud.");
        }

        public void EndTurn()
        {
            if (IsDead)
                Die();
        }
    }
}
