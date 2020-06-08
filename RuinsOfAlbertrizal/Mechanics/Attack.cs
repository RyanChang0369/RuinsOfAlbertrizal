using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{
    [Serializable]
    public class Attack : ObjectOfAlbertrizal, ITurnBasedObject
    {
        public List<Buff> Buffs { get; set; }

        public int[] StatLoss { get; set; }

        public int[] StatCostToUser { get; set; }

        public int CoolDown { get; set; }

        public int ChargeUp { get; set; }

        /// <summary>
        /// Turns since last attacking with this attack, or 0 if not applicable.
        /// </summary>
        public int TurnSinceAttacked { get; set; }

        /// <summary>
        /// Turns since charging an attack, or 0 if not applicable.
        /// </summary>
        public int TurnsSinceBeginCharge { get; set; }

        public bool IgnoresArmor { get; set; }

        public enum TargetType
        {
            [Description("Cannot target friendly characters")]
            EnemiesOnly,
            //[Description("Can only target unfriendlies in front row")]
            //FirstRowOnly,
            //[Description("Can only target unfriendlies in back row")]
            //LastRowOnly,
            //[Description("Can only target unfriendlies in the first two rows")]
            //FirstTwoRows,
            //[Description("can only target unfriendlies in the back two rows")]
            //LastTwoRows,
            [Description("Cannot target unfriendlies")]
            FriendliesOnly,
            [Description("Can target anything")]
            Everything,
            [Description("Can only target yourself")]
            Self
        }

        public TargetType TypeOfTarget { get; set; }

        public Attack()
        {
            StatLoss = new int[GameBase.NumStats];
            StatCostToUser = new int[GameBase.NumStats];
            Buffs = new List<Buff>();
        }

        [XmlIgnore]
        public bool IsCharged
        {
            get
            {
                return ChargeUp <= TurnsSinceBeginCharge;
            }
        }

        public bool CanCharacterAttack(Character character)
        {
            if (character.CurrentStats[1] - StatCostToUser[1] < 0)
                return false;
            else
                return (CoolDown <= TurnSinceAttacked) && IsCharged;
        }

        /// <summary>
        /// Gets the amount of stats lost over the lifespan of this attack plus its buffs
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <returns></returns>
        public int[] GetLifetimeStatGain(Character target)
        {
            int[] totalStatLoss = StatLoss;
            int[] buffStatGain = new int[GameBase.NumStats];

            foreach (Buff buff in Buffs)
            {
                buffStatGain = ArrayMethods.AddArrays(buffStatGain, buff.GetLifetimeStatGain(target));
            }

            for (int i = 0; i < GameBase.NumStats; i++)
            {
                buffStatGain[i] = buffStatGain[i] * -1;
            }

            return totalStatLoss;
        }

        /// <summary>
        /// Gets the amount of a specified stat gained over the lifespan of this buff
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <param name="statIndex">The index of the stat to evaluate</param>
        /// <returns></returns>
        public int GetLifetimeStatGain(Character target, int statIndex)
        {
            int total = 0;

            total += StatGain[statIndex];
            total += (int)Math.Round(PercentStatGain[statIndex] * target.CurrentStats[statIndex]);

            return total;
        }

        /// <summary>
        /// Starts the attack.
        /// </summary>
        /// <param name="attacker">The character doing the attacking.</param>
        /// <param name="target">The character being attacked.</param>
        public void BeginAttack(Character attacker, Character target)
        {
            if (CanCharacterAttack(attacker))
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                for (int i = 0; i < StatCostToUser.Length; i++)
                {
                    attacker.AppliedStats[i] -= StatCostToUser[i];
                }

                target.GetAttacked(this);
            }
            else if (!IsCharged)
            {
                //Begin charge
                TurnsSinceBeginCharge++;
            }
        }

        /// <summary>
        /// Deals damage
        /// </summary>
        /// <param name="target"></param>
        public void DealStats(Character target)
        {
            for (int i = 0; i < StatLoss.Length; i++)
            {
                target.AppliedStats[i] -= StatLoss[i]; 
            }
        }

        public void DealBuffs(Character target)
        {
            foreach (Buff buff in Buffs)
            {
                target.AppliedBuffs.Add(buff);
            }
        }

        /// <summary>
        /// Returns a 2D list/array thing with the player attack indexes on row 1 and the enemy attack indexes on row 2
        /// </summary>
        /// <returns></returns>
        public List<int>[] GetAttackIndexes(Character attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            List<int>[] targetIndexes = new List<int>[2];

            List<int> playerTargetIndexes = new List<int>();
            List<int> enemyTargetIndexes = new List<int>();
            
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                if (CanTargetCharacter(attacker, activePlayers[i]))
                    playerTargetIndexes.Add(i);

                if (CanTargetCharacter(attacker, activeEnemies[i]))
                    playerTargetIndexes.Add(i);
            }

            targetIndexes[0] = playerTargetIndexes;
            targetIndexes[1] = enemyTargetIndexes;

            return targetIndexes;
        }

        public bool CanTargetCharacter(Character attacker, Character target)
        {
            if (target == null || attacker == null || attacker.IsDead)
                return false;

            if (target.IsDead)
            {
                //Dead chracters are only targetable if they can be revived
                bool doesNotHaveRevive = true;
                foreach (Buff buff in Buffs)
                {
                    if (buff.TypeOfBuff == Buff.BuffType.Revive)
                    {
                        doesNotHaveRevive = false;
                        break;
                    }
                }

                if (doesNotHaveRevive)
                    return false;
            }

            switch (TypeOfTarget)
            {
                case TargetType.EnemiesOnly:
                    return attacker.GetType() != target.GetType();
                case TargetType.FriendliesOnly:
                    return attacker.GetType() == target.GetType();
                case TargetType.Everything:
                    return true;
                case TargetType.Self:
                    return attacker.Equals(target);
                default:
                    throw new ArgumentException("TypeOfTarget not a value of TargetType.");
            }
        }

        public void EndTurn()
        {
            TurnSinceAttacked++;
        }

        public void StartTurn()
        {
            
        }
    }
}
