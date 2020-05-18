using RuinsOfAlbertrizal.AIs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    /// <summary>
    /// Buffs and debuffs
    /// </summary>
    public class Buff : IconedObjectOfAlbertrizal, ITurnBasedObject
    {
        /// <summary>
        /// How many turns will this last for?
        /// </summary>
        public int Duration { get; set; }

        [XmlIgnore]
        public int LeveledDuration { get => Duration * (1 + Level); }

        public int TurnsPassed { get; set; }

        [XmlIgnore]
        public bool HasEnded { get => TurnsPassed >= LeveledDuration; }

        /// <summary>
        /// See GameBase.Stats for values
        /// </summary>
        public int[] StatGain { get; set; }

        public enum BuffType
        {
            [Description("Normal buff type. No special features.")]
            Normal,
            [Description("Kills the character receiving this buff by dealing damage equal to its remaining health times 10.")]
            InstaKill,
            [Description("Revives the receiving character with 20% health.")]
            Revive,
            [Description("Remoev all of the buffs of the receiving character.")]
            Cleanse,
            [Description("If the receiving character's health is above 1, then negate any lethal attack and set health to 1.")]
            LastHope,
            [Description("The receiving character cannot take any damage or receive any buffs.")]
            Invunerable
        }

        public BuffType TypeOfBuff { get; set; }

        [XmlIgnore]
        public int[] LeveledStatGain
        {
            get
            {
                int[] leveledStats = new int[5];

                for (int i = 0; i < StatGain.Length; i++)
                {
                    leveledStats[i] = (int)Math.Round(StatGain[i] * 1.2);
                }

                return leveledStats;
            }
        }

        /// <summary>
        /// Changes the AI
        /// </summary>
        public AI.AIStyle AIChange { get; set; }

        /// <summary>
        /// Use to alter the duration of the effect. Duration is mutiplied by the level + 1 (level starts at 0).
        /// </summary>
        public int Level { get; set; }

        public Buff()
        {
            StatGain = new int[5];
            TypeOfBuff = new BuffType();
        }

        public void EndTurn()
        {
            TurnsPassed++;
        }

        public void StartTurn()
        {
            
        }
    }
}
