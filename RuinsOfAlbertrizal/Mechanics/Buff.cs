using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Characters;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    /// <summary>
    /// Buffs and debuffs
    /// </summary>
    
    public class Buff : IconedObjectOfAlbertrizal, IRoundBasedObject
    {
        public override string DisplayName
        {
            get
            {
                return $"Level {Level} {Name}";
            }
        }

        /// <summary>
        /// How many turns will this last for?
        /// </summary>
        public int Duration { get; set; }

        [XmlIgnore]
        public int LeveledDuration { get => Duration * (1 + Level); }

        public int RoundsPassed { get; set; }

        [XmlIgnore]
        public bool HasEnded { get => RoundsPassed >= LeveledDuration; }

        /// <summary>
        /// See GameBase.Stats for values
        /// </summary>
        public int[] StatGain { get; set; }

        /// <summary>
        /// If this value is set, then ignore StatGain
        /// </summary>
        public double[] PercentStatGain { get; set; }

        public enum BuffType
        {
            [Description("Normal buff type. No special features.")]
            Normal,
            [Description("Revives the receiving character and heals to 1 health.")]
            Revive,
            [Description("Removes all of the buffs of the receiving character.")]
            Cleanse,
            [Description("If the receiving character's health is above 1, then negate any lethal attack and set health to 1." +
                "If an attack causes this buff, then this buff will come into effect if the receiving character's health " +
                "drops below 0.")]
            LastHope,
            [Description("The receiving character cannot take any damage or receive any buffs. " +
                "However, if an attack causes this buff, then the receiving character WILL take damage from the attack.")]
            Invunerable
        }

        public BuffType TypeOfBuff { get; set; }

        [XmlIgnore]
        public int[] LeveledStatGain
        {
            get
            {
                int[] leveledStats = new int[GameBase.NumStats];

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
            AIChange = AI.AIStyle.NoChange;
            StatGain = new int[GameBase.NumStats];
            PercentStatGain = new double[10];
            TypeOfBuff = new BuffType();
        }


        public void DealStats(Character character)
        {
            for (int i = 0; i < GameBase.NumStats; i++)
            {
                character.AppliedStats[i] += StatGain[i];
                character.AppliedStats[i] += (int)Math.Round(character.AppliedStats[i] * PercentStatGain[i]);
            }

            if (TypeOfBuff == BuffType.Revive && character.IsDead)
                Revive(character);
            else if (TypeOfBuff == BuffType.Cleanse)
                Cleanse(character);
        }

        /// <summary>
        /// Gets the amount of stats gained over the lifespan of this buff
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <returns></returns>
        public int[] GetLifetimeStatGain(Character target)
        {
            int[] totalStatGain = new int[GameBase.NumStats];

            for (int i = 0; i < GameBase.NumStats; i++)
            {
                totalStatGain[i] += StatGain[i];
                totalStatGain[i] += (int)Math.Round(PercentStatGain[i] * target.CurrentStats[i]);
            }

            return totalStatGain;
        }

        /// <summary>
        /// Gets the amount of a specified stat gained over the lifespan of this buff
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <param name="statIndex">The index of the stat to evaluate</param>
        /// <returns></returns>
        public int GetLifetimeStatGain(Character target, GameBase.Stats stat)
        {
            int total = 0;
            int statIndex = (int)stat;

            total += StatGain[statIndex];
            total += (int)Math.Round(PercentStatGain[statIndex] * target.CurrentStats[statIndex]);

            return total;
        }

        public void StartRound()
        {

        }

        public void EndRound()
        {
            RoundsPassed++;
        }

        private void Revive(Character character)
        {
            character.AppliedStats[0] = 1;
        }

        private void Cleanse(Character character)
        {
            character.AppliedBuffs.Clear();
        }

        public void EndTurn()
        {
            
        }

        public void StartTurn()
        {
            
        }
    }
}
