using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Mechanics
{

    public class Attack : ObjectOfAlbertrizal, IRoundBasedObject
    {
        public List<Buff> Buffs { get; set; }

        public int[] StatLoss { get; set; }

        public int[] StatCostToUser { get; set; }

        /// <summary>
        /// Affects the animation of the attack. Does not affect range.
        /// </summary>
        public bool IsRanged { get; set; }

        [XmlIgnore]
        public bool HasBuffs => Buffs != null && Buffs.Count > 0;

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

        /// <summary>
        /// Can target multiple characters
        /// </summary>
        public bool MultiTarget { get; set; }

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

        public bool IsCharged()
        {
            return ChargeUp <= TurnsSinceBeginCharge;
        }

        public bool IsCooledDown()
        {
            return CoolDown <= TurnsSinceBeginCharge;
        }

        /// <summary>
        /// Returns true if the attack can be used by the provided character (has enough mana, etc).
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool CanBeUsedBy(Character character)
        {
            return IsReadyForUse(character) && IsCharged();
        }

        /// <summary>
        /// Returns true if the attack can be used by the provided character (has enough mana, etc).
        /// Returns true even if not charged, as an enemy will be allowed to charge it.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool IsReadyForUse(Character character)
        {
            if (character.CurrentStats[1] - StatCostToUser[1] < 0)
                return false;
            else
                return IsCooledDown();
        }

        /// <summary>
        /// Gets the amount of stats lost over the lifespan of this attack plus its buffs
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <returns></returns>
        public int[] GetLifetimeStatLoss(Character target)
        {
            int[] totalStatLoss = (int[])StatLoss.Clone();
            int[] buffStatGain = new int[GameBase.NumStats];

            foreach (Buff buff in Buffs)
            {
                buffStatGain = ArrayMethods.AddArrays(buffStatGain, buff.GetLifetimeStatGain(target));
            }

            for (int i = 0; i < GameBase.NumStats; i++)
            {
                buffStatGain[i] = buffStatGain[i] * -1;
            }

            return ArrayMethods.AddArrays(totalStatLoss, buffStatGain);
        }

        /// <summary>
        /// Gets the amount of a specified stat lost over the lifespan of this attack and its buffs
        /// </summary>
        /// <param name="target">The character whose CurrentStats will be evaluated
        /// to determine the stat gain from PercentStatGain</param>
        /// <param name="statIndex">The index of the stat to evaluate</param>
        /// <returns></returns>
        public int GetLifetimeStatLoss(Character target, GameBase.Stats stat)
        {
            int total = 0;
            int statIndex = (int)stat;

            total += StatLoss[statIndex];

            foreach (Buff buff in Buffs)
            {
                total -= buff.GetLifetimeStatGain(target, stat);
            }

            return total;
        }

        ///// <summary>
        ///// Starts the attack.
        ///// </summary>
        ///// <param name="attacker">The character doing the attacking.</param>
        ///// <param name="targets">The character being attacked.</param>
        ///// <param name="useStatCost">If false, then do not use StatCostToUser</param>
        ///// <exception cref="ArgumentException"></exception>
        //public void BeginAttack(Character attacker, List<Character> targets, bool useStatCost = true)
        //{
        //    if (CanBeUsedBy(attacker))
        //    {
        //        TurnSinceAttacked = 0;
        //        TurnsSinceBeginCharge = 0;

        //        if (useStatCost)
        //        {
        //            for (int i = 0; i < StatCostToUser.Length; i++)
        //            {
        //                attacker.AppliedStats[i] -= StatCostToUser[i];
        //            }
        //        }

        //        GameBase.CurrentGame.CurrentBattleField.NotifyAttackBegin(this, attacker);

        //        foreach (Character target in targets)
        //        {
        //            if (!CanTargetCharacter(attacker, target))
        //                throw new ArgumentException($"Character {attacker.DisplayName} cannot target {target.DisplayName}.");
        //            else
        //                target.GetAttacked(this);
        //        }
        //    }
        //    else if (!IsCharged)
        //    {
        //        //Begin/Continue charge
        //        TurnsSinceBeginCharge++;
        //        GameBase.CurrentGame.CurrentBattleField.StoredMessage.Add($"{attacker.DisplayName} is charging attack {DisplayName}");
        //        attacker.AttackToCharge = this;
        //    }
        //}

        /// <summary>
        /// Starts the attack
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <param name="useStatCost">If false, then do not use StatCostToUser</param>
        /// <exception cref="CannotTargetException"></exception>
        /// <exception cref="NotEnoughManaException"></exception>
        public void BeginAttack(Character attacker, Character target, bool useStatCost = true)
        {
            if (!CanTargetCharacter(attacker, target))
                throw new CannotTargetException($"Character {attacker.DisplayName} cannot target {target.DisplayName}.");
            else if (StatCostToUser[1] > attacker.CurrentStats[1])
                throw new NotEnoughManaException($"Character {attacker.DisplayName} does not have the required amount of mana to use this attack.");

            if (CanBeUsedBy(attacker))
            {
                TurnSinceAttacked = 0;
                TurnsSinceBeginCharge = 0;

                if (useStatCost)
                {
                    for (int i = 0; i < StatCostToUser.Length; i++)
                    {
                        attacker.AppliedStats[i] -= StatCostToUser[i];
                    }
                }

                GameBase.CurrentGame.CurrentBattleField.NotifyAttackBegin(this, attacker, false);

                target.GetAttacked(this);
            }
            else if (!IsCharged())
            {
                //Begin/Continue charge
                Charge(attacker);
            }
        }

        public void Charge(Character attacker)
        {
            TurnsSinceBeginCharge++;
            attacker.AttackToCharge = this;
            GameBase.CurrentGame.CurrentBattleField.NotifyAttackBegin(this, attacker, true);
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
                    enemyTargetIndexes.Add(i);

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

        public bool CanTargetEverything(Character attacker, Character[] targets)
        {
            if (attacker == null || attacker.IsDead)
                return false;

            foreach (Character target in targets)
            {
                if (CanTargetCharacter(attacker, target))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Finds most damaging attack, in terms of the given stat, that the attacker can use, or returns null if no such attack can be found.
        /// </summary>
        public static Attack FindStrongestAttack(Character attacker, Character target, GameBase.Stats stat)
        {
            return FindStrongestAttack(attacker, target, stat, attacker.AllAttacks);
        }

        /// <summary>
        /// Finds most damaging attack that the attacker can use from a list of provided attacks, or returns null if no such attack can be found.
        /// </summary>
        public static Attack FindStrongestAttack(Character attacker, Character target, GameBase.Stats stat, List<Attack> attacks)
        {
            Attack strongestAttack = null;
            int strongestStat = 0;

            foreach (Attack attack in attacks)
            {
                if (!attack.IsCooledDown() || !attack.CanTargetCharacter(attacker, target))
                    continue;
                else if (attack.GetLifetimeStatLoss(target, stat) > strongestStat)
                {
                    strongestAttack = attack;
                    strongestStat = strongestAttack.GetLifetimeStatLoss(target, stat);
                }
            }

            return strongestAttack;
        }

        /// <summary>
        /// Finds the attack that heals the most, or returns null if no healing attacks can be found.
        /// </summary>
        /// <param name="healer"></param>
        /// <param name="target"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        public static Attack FindBestHealingAttack(Character healer, Character target, GameBase.Stats stat)
        {
            return FindBestHealingAttack(healer, target, stat, healer.AllAttacks);
        }

        /// <summary>
        /// Finds the attack that heals the most, or returns null if no healing attacks can be found.
        /// </summary>
        /// <param name="healer"></param>
        /// <param name="target"></param>
        /// <param name="stat"></param>
        /// <param name="attacks"></param>
        /// <returns></returns>
        public static Attack FindBestHealingAttack(Character healer, Character target, GameBase.Stats stat, List<Attack> attacks)
        {
            Attack mostHealyAttack = null;
            int weakestStat = 0;

            foreach (Attack attack in attacks)
            {
                if (!attack.IsCooledDown() || !attack.CanTargetCharacter(healer, target))
                    continue;
                else if (attack.GetLifetimeStatLoss(target, stat) < weakestStat)
                {
                    mostHealyAttack = attack;
                    weakestStat = mostHealyAttack.GetLifetimeStatLoss(target, stat);
                }
            }

            return mostHealyAttack;
        }

        public void EndTurn()
        {
            TurnSinceAttacked++;
        }

        public void StartTurn()
        {
            
        }

        public void StartRound()
        {
            
        }

        public void EndRound()
        {
            
        }
    }
}
