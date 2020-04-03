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

        public int[] BaseStats { get; set; }

        public int[] CurrentStats { get; set; }

        public List<int> Abilities { get; set; }

        public List<Buff> Buffs { get; set; }

        [XmlIgnore]
        public bool IsDead { get => CurrentStats[0] <= 0; }

        public Character()
        {

        }

        /// <summary>
        /// Creates a new charecter with the following values, abilities, and buffs
        /// </summary>
        /// <param name="generalName">The name of the "species" such as human or orc</param>
        /// <param name="specificName">The proper name, such as Bob or Robert</param>
        /// <param name="baseStats">[0]=HP, [1]=Mana, [2]=Mana, [3]=Def, [4]=Spd</param>
        /// <param name="currentStats">[0]=HP, [1]=Mana, [2]=Mana, [3]=Def, [4]=Spd</param>
        /// <param name="abilities">See Ability class</param>
        public Character(string generalName, string specificName, int[] baseStats, int[] currentStats,
            List<int> abilities, List<Buff> buffs)
        {
            GeneralName = generalName;
            SpecificName = specificName;
            BaseStats = baseStats;
            CurrentStats = currentStats;

            Abilities = abilities;
            Buffs = buffs;
        }

        /// <summary>
        /// Creates a new charecter with the following values and abilities
        /// </summary>
        /// <param name="generalName">The name of the "species" such as human or orc</param>
        /// <param name="specificName">The proper name, such as Bob or Robert</param>
        /// <param name="baseStats">[0]=HP, [1]=Mana, [2]=Mana, [3]=Def, [4]=Spd</param>
        /// <param name="abilities">See Ability class</param>
        public Character(string generalName, string specificName, int[] baseStats,
            List<int> abilities) : this(generalName, specificName, baseStats, baseStats, abilities, null)
        {

        }

        /// <summary>
        /// Creates a new charecter with the following values and no abilities
        /// </summary>
        /// <param name="generalName">The name of the "species" such as human or orc</param>
        /// <param name="specificName">The proper name, such as Bob or Robert</param>
        /// <param name="baseStats">[0]=HP, [1]=Mana, [2]=Mana, [3]=Def, [4]=Spd</param>
        public Character(string generalName, string specificName, int[] baseStats)
            : this(generalName, specificName, baseStats, null)
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
            foreach (Buff buff in Buffs)
            {
                CurrentStats = ArrayMethods.AddArrays(CurrentStats, buff.StatGain);
                if (buff.HasEnded)
                    Buffs.Remove(buff);
            }

            if (IsDead)
                Die();
        }
    }
}
