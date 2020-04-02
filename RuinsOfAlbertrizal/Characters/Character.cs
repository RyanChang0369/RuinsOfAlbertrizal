using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// The proper name
        /// </summary>
        public string SpecificName { get; set; }

        /// <summary>
        /// Maximum health
        /// </summary>
        public int BaseHP { get; set; }

        /// <summary>
        /// Maximum mana
        /// </summary>
        public int BaseMana { get; set; }

        /// <summary>
        /// How much damage should be subtracted from incoming damage?
        /// </summary>
        public int BaseDef { get; set; }

        /// <summary>
        /// How much damage should this thing do?
        /// </summary>
        public int BaseDmg { get; set; }

        /// <summary>
        /// How much tiles can this boi travel in one turn?
        /// </summary>
        public int BaseSpd { get; set; }

        /// <summary>
        /// How high the character can jump in blocks
        /// </summary>
        public double BaseJump { get; set; }

        public int CurrentHP { get; set; }
        public int CurrentMana { get; set; }
        public int CurrentDef { get; set; }
        public int CurrentDmg { get; set; }
        public int CurrentSpd { get; set; }
        public double CurrentJump { get; set; }

        public List<int> Abilities { get; set; }

        public Character()
        {

        }

        public Character(string generalName, string specificName, int baseHP, int baseMana, int baseDef, int baseDmg, int baseSpd,
            double baseJump, List<int> abilities)
        {
            GeneralName = generalName;
            SpecificName = specificName;
            BaseHP = baseHP;
            BaseDef = baseDef;
            BaseDmg = baseDmg;
            BaseSpd = baseSpd;
            BaseJump = baseJump;

            CurrentHP = BaseHP;
            CurrentDef = BaseDef;
            CurrentMana = BaseMana;
            CurrentDmg = BaseDmg;
            CurrentSpd = BaseSpd;
            CurrentJump = BaseJump;

            Abilities = abilities;
        }
        public Character(string generalName, string specificName, int baseHP, int baseMana, int baseDef, int baseDmg, int baseSpd,
            double baseJump, List<int> abilities,
            int currentHP, int currentMana, int currentDef, int currentDmg, int currentSpd, int currentJump)
        {
            GeneralName = generalName;
            SpecificName = specificName;
            BaseHP = baseHP;
            BaseDef = baseDef;
            BaseDmg = baseDmg;
            BaseSpd = baseSpd;
            BaseJump = baseJump;

            CurrentHP = currentHP;
            CurrentDef = currentDef;
            CurrentMana = currentMana;
            CurrentDmg = currentDmg;
            CurrentSpd = currentSpd;
            CurrentJump = currentJump;

            Abilities = abilities;
        }
    }
}
