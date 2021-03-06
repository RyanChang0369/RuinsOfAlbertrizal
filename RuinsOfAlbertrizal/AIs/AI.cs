﻿using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Exceptions;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.AIs
{
    public class AI
    {
        public static int counter = 0;

        private static readonly double[] statPercentages_Timid1 = { 0.5, 0.0, 0.3, 0.25, 0.4, 0.25 };
        private static readonly double[] statPercentages_Timid2 = { 0.2, 0.0, 0.1, 0.05, 0.2, 0.05 };

        public enum AIStyle
        {
            [Description(
                "The player controls this character." +
                " Only works with a player character, and is treated as NoAI everywhere else.")]
            Player = 0,
            [Description("A special AI that does nothing. Use when you do not want to change a character's AI in a buff. Is treated as NoAI everywhere else.")]
            NoChange = 1,
            [Description("No AI. Cannot move, regenerate mana, or attack.")]
            NoAI = 2,
            [Description(
                "Attacks player with the most damaging attacks possible." +
                " Moves so that its most damaging attack or attacks is in range of the player." +
                " Attacks twice if doing so will deal the most damage to the player." +
                " No regard to health." +
                " Recovers mana only if unable to attack." +
                " Cannot use items.")]
            Berserk = 10,
            [Description(
                "Attacks player with the most damaging attacks possible." +
                " Moves so that its most damaging attack or attacks is in range of the player." +
                " Attacks twice if doing so will deal the most damage to the player." +
                " Heals with healing items and/or spells if below 40% health." +
                " Recovers mana only if unable to attack." +
                " Uses items to recover health and mana.")]
            Berserk_UseItem = 11,
            [Description(
                "Tries to avoid players. " +
                "Heals with healing items.")]
            Timid = 20,
            [Description(
                "Similar to Timid. " +
                "Heals other enemies if they are below 75% health. Identical to Timid otherwise.")]
            Healer = 30,
            //[Description(
            ////"Tries to stay above the player. " +
            //"Same as timid."
            ////"Moves so it is out of range of player. Moves on its first turn. " +
            ////"Attacks twice only if player is very far away (twice the range of the player). " +
            ////"Only heals if out of range of player. " +
            ////"Recovers when unable to attack and player is out of range or when player is very far away (twice the player's movement range). " +
            ////"Uses items when unable to attack and player is out of range or when player is very far away (twice the player's movement range). "
            //)]
            //Flying = 40,
            //[Description("Same as healer.")]
            //Flying_Healer = 41
        }

        [XmlIgnore]
        public static IList<AIStyle> AIStyleNames
        {
            get
            {
                return Enum.GetValues(typeof(AIStyle)).Cast<AIStyle>().ToList();
            }
        }

        /// <summary>
        /// Selects a target and attacks it.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="activePlayers"></param>
        /// <param name="activeEnemies"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static void SelectTarget(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            counter++;
            //If charging an attack, let it charge.
            try
            {
                attacker.TryContinueCharge();
                return;
            }
            catch (ArgumentException)
            {

            }

            //If enemy has no attacks, try to run
            if (attacker.AllAttacks.Count < 1)
            {
                attacker.Run(GameBase.CurrentGame.CurrentBattleField);
                return;
            }

            switch (attacker.AIStyle)
            {
                case AIStyle.NoAI:
                case AIStyle.NoChange:
                case AIStyle.Player:
                    throw new ArgumentException("The current AIStyle forbids attacking");
                case AIStyle.Berserk:
                    AIStyle_Berserk(attacker, activePlayers);
                    break;
                case AIStyle.Berserk_UseItem:
                    AIStyle_BerserkUseItem(attacker, activePlayers);
                    break;
                case AIStyle.Timid:
                //case AIStyle.Flying:
                    AIStyle_Timid(attacker, activePlayers);
                    break;
                case AIStyle.Healer:
                //case AIStyle.Flying_Healer:
                    AIStyle_Healer(attacker, activePlayers, activeEnemies);
                    break;
            }
        }

        /// <summary>
        /// Returns true if a consumable was consumed or a healing attack was used and false otherwise
        /// </summary>
        /// <param name="user">The enemy consuming the consumable</param>
        /// <param name="statPercentages">An array that contains all of the percent stat at which the user will consume the consumable.</param>
        /// <returns>True if a consumable was consumed and false otherwise</returns>
        public static bool AutoConsume(Enemy user, double[] statPercentages)
        {
            if (user.InventoryConsumables.Count < 1)
                return false;

            double[] percentStats = user.PercentStats;

            for (int i = 0; i < GameBase.NumStats; i++)
            {
                try
                {
                    if (percentStats[i] < statPercentages[i])
                    {
                        Consumable consumable = FindUsableConsumable(user, (GameBase.Stats)i);
                        Attack bestHealyAttack = user.FindBestHealingAttack(user, (GameBase.Stats)i);

                        if (consumable != null && bestHealyAttack != null)
                        {
                            if (consumable.GetLifetimeStatGain(user, (GameBase.Stats)i) > (bestHealyAttack.GetLifetimeStatLoss(user, (GameBase.Stats)i) * -1) / (bestHealyAttack.ChargeUp + 1))
                            {
                                user.Consume(consumable);
                            }
                            else
                            {
                                user.DoAttack(bestHealyAttack, user);
                            }
                            
                            return true;
                        }
                        else if (consumable != null)
                        {
                            user.Consume(consumable);
                            return true;
                        }
                        else if (bestHealyAttack != null)
                        {
                            user.DoAttack(bestHealyAttack, user);
                            return true;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }
            }


            return false;
        }

        /// <summary>
        /// Returns true if a consumable was consumed or a healing attack was used and false otherwise
        /// </summary>
        /// <param name="user">The enemy consuming the consumable</param>
        /// <param name="stat">The stat to evaluate</param>
        /// <returns>True if a consumable was consumed and false otherwise</returns>
        public static bool AutoConsume(Enemy user, GameBase.Stats stat, double statPercentage)
        {
            if (user.InventoryConsumables.Count < 1)
                return false;

            int i = (int)stat;

            if (user.PercentStats[i] >= statPercentage)
                return false;

            Consumable consumable = FindUsableConsumable(user, stat);
            Attack bestHealyAttack = user.FindBestHealingAttack(user, stat);

            if (consumable != null && bestHealyAttack != null)
            {
                if (consumable.GetLifetimeStatGain(user, stat) > (bestHealyAttack.GetLifetimeStatLoss(user, stat) * -1) / (bestHealyAttack.ChargeUp + 1))
                {
                    user.Consume(consumable);
                }
                else
                {
                    user.DoAttack(bestHealyAttack, user);
                }

                return true;
            }
            else if (consumable != null)
            {
                user.Consume(consumable);
                return true;
            }
            else if (bestHealyAttack != null)
            {
                user.DoAttack(bestHealyAttack, user);
                return true;
            }
            else
                return false;
        }

        public static Consumable FindUsableConsumable(Enemy user, GameBase.Stats stat)
        {
            if (user.InventoryConsumables.Count < 1)
                return null;

            int statToRecover = user.ArmoredStats[(int)stat] - user.CurrentStats[(int)stat];
            int minimumDifference = int.MaxValue;
            Consumable selectedConsumable = null;

            //Find the consumable that completly heals the user without wasting very good consumables
            foreach (Consumable consumable in user.InventoryConsumables)
            {
                //The higher the user's intellect, the less likely they will use a potion with more negative side effects.
                //This check is ignored if the potion's util is above or equal to 10
                if (user.ArmoredStats[(int)GameBase.Stats.Int] > consumable.GetUtils(user) && consumable.GetUtils(user) < 10)
                {
                    continue;
                }

                int hpGain = consumable.GetLifetimeStatGain(user, stat);
                if (hpGain > statToRecover)
                {
                    if (statToRecover - hpGain < minimumDifference)
                    {
                        selectedConsumable = consumable;
                        minimumDifference = statToRecover - hpGain;
                    }
                }
            }

            //Consumable that can fully replenish does not exist.
            if (selectedConsumable == null)
            {
                int maxStat = 0;

                foreach (Consumable consumable in user.InventoryConsumables)
                {
                    //The higher the user's intellect, the less likely they will use a potion with more negative side effects.
                    //This check is ignored if the potion's util is above or equal to 10
                    if (user.ArmoredStats[(int)GameBase.Stats.Int] > consumable.GetUtils(user) && consumable.GetUtils(user) < 10)
                    {
                        continue;
                    }
                    else if (consumable.GetLifetimeStatGain(user, stat) > maxStat)
                    {
                        selectedConsumable = consumable;
                        maxStat = consumable.GetLifetimeStatGain(user, stat);
                    }
                }
            }

            return selectedConsumable;

            //If recovering mana is better, just recover mana
            //if (selectedConsumable != null && stat == GameBase.Stats.Mana && (int)Math.Round(user.LeveledStats[1] * 0.3) > selectedConsumable.GetLifetimeStatGain(user, GameBase.Stats.Mana))
            //    user.RecoverMana();
            ////Else consume the item if it is not null
            //else if (selectedConsumable != null)
            //    user.Consume(selectedConsumable);
            ////Else throw exception
            //else
            //    return null;
        }

        public static void AIStyle_Berserk(Enemy attacker, Player[] activePlayers)
        {
            //Find player with lowest hp and select that as target
            Player target = activePlayers.FindMin(t => t.CurrentStats[0]);

            double fateSelector = RNG.GetRandomDouble();

            Attack attack;

            List<Attack> multiTargetAttacks = attacker.GetMultiTargetAttacks(activePlayers);

            if (multiTargetAttacks.Count < 1)
            {
                //Attacker does not multitarget attacks
                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP);
            }
            else if (fateSelector < 0.85 && target.PercentStats[0] < 0.35)
            {
                //85% chance that the enemy will use the strongest multitargeting attack instead of the strongest single attack
                //if target is below 35% health

                attack = Attack.FindStrongestAttack(attacker, target, activePlayers, GameBase.Stats.HP,
                    multiTargetAttacks, false);
            }
            else if (fateSelector < 0.1)
            {
                //10% chance that the enemy will use the strongest multitargeting attack anyways

                attack = Attack.FindStrongestAttack(attacker, target, activePlayers, GameBase.Stats.HP,
                    multiTargetAttacks, false);
            }
            else
            {
                //Attack weakest player with strongest attack
                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP);
            }

            if (attack == null)
            {
                //No attacks in range. Move closer.
                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP, attacker.AllAttacks, true);
                Pathfinding.PathFind_Direct(attacker, target, attack.Range);
                return;
            }

            try
            {
                attacker.DoAttack(attack, target);
            }
            catch (NotEnoughManaException)
            {
                attacker.RecoverMana();
            }
        }

        public static void AIStyle_BerserkUseItem(Enemy attacker, Player[] activePlayers)
        {
            if (attacker.PercentStats[0] < 0.4)
            {
                if (AutoConsume(attacker, GameBase.Stats.HP, 0.4))
                    return;
            }

            AIStyle_Berserk(attacker, activePlayers);
        }

        public static void AIStyle_Timid(Enemy attacker, Player[] activePlayers)
        {
            double[] percentStats = attacker.PercentStats;

            List<Player> playersWithinRange = CharactersWithinRange(attacker, attacker.AllAttacks, activePlayers);

            //Step 1: Find closest player and select that as target
            Player target = activePlayers[0];
            double minDistance = int.MaxValue;

            foreach (Player player in playersWithinRange)
            {
                if (player == null)
                    continue;

                double distance = target.DirectDistanceFrom(player);
                if (minDistance < distance)
                {
                    target = player;
                    minDistance = distance;
                }
            }

            //Step 2: Find longest ranged attack.
            Attack longestRangeAttack = attacker.AllAttacks.FindMax(t => t.Range);

            //Start logic for AI Style

            if (attacker.IsWellOff(statPercentages_Timid1))
            {
                if (playersWithinRange.Count < 1)
                {
                    //No players within range.
                    //Path find towards players

                    Pathfinding.PathFind_Direct(attacker, target, longestRangeAttack.Range, false);
                    return;
                }
                else
                {
                    //Attack weakest player
                    target = playersWithinRange.FindMin(t => t.CurrentStats[0]);
                    Attack selectedAttack = attacker.FindStrongestAttack(target, GameBase.Stats.HP);
                    attacker.DoAttack(selectedAttack, target);
                    return;
                }
            }
            else if (attacker.IsWellOff(statPercentages_Timid2))
            {
                RandomEventChooser chooser = new RandomEventChooser
                (
                    new List<RandomEvent>
                    {
                        new RandomEvent("Attack", percentStats[0]),
                        new RandomEvent("Heal", 0.5)
                    }
                );

                string selectedEvent = chooser.GetSelected().ToString();

                if (selectedEvent.Equals("Attack") && target != null)
                {
                    Attack selectedAttack = attacker.FindStrongestAttack(target, GameBase.Stats.HP);

                    //Find distance between attacker and target
                    double targetDistance = MiscMethods.DistanceFormula(target.BattleFieldLocation, attacker.BattleFieldLocation);

                    //If distance between targetDistance and range of selected attack is negligible, attack
                    if (selectedAttack.Range - targetDistance < 2)
                    {
                        //Attack weakest player
                        target = playersWithinRange.FindMin(t => t.CurrentStats[0]);
                        
                        if (target != null)
                        {
                            attacker.DoAttack(selectedAttack, target);
                            return;
                        }
                        else if (AutoConsume(attacker, statPercentages_Timid1))
                        {
                            return;
                        }
                        else
                        {
                            //Change to something else?
                            attacker.RecoverMana();
                        }
                    }
                    //Else, move away
                    else
                    {
                        try
                        {
                            Pathfinding.PathFind_Direct(attacker, target, selectedAttack.Range, true);
                        }
                        catch (DidNotMoveException)
                        {
                            //Attack weakest player
                            target = playersWithinRange.FindMin(t => t.CurrentStats[0]);

                            if (target != null)
                            {
                                attacker.DoAttack(selectedAttack, target);
                                return;
                            }
                            else if (AutoConsume(attacker, statPercentages_Timid1))
                            {
                                return;
                            }
                            else
                            {
                                //Change to something else?
                                attacker.RecoverMana();
                            }
                        }

                        return;
                    }
                }
                else
                {
                    HealWithItemsOrAttacks(attacker, statPercentages_Timid1);
                    return;
                }
            }
            else
            {
                RandomEventChooser chooser = new RandomEventChooser
                (
                    new List<RandomEvent>
                    {
                        new RandomEvent("Heal", 0.75),
                        new RandomEvent("RunFromPlayer", 0.25),
                        new RandomEvent("RunFromBattleField", 0.25)
                    }
                );

                string eventSelector = chooser.GetSelected().ToString();

                switch (eventSelector)
                {
                    case "Heal":
                        HealWithItemsOrAttacks(attacker, statPercentages_Timid1);
                        return;
                    case "RunFromPlayer":
                        Pathfinding.PathFind_RunFromPlayers(attacker, activePlayers.ToList());
                        return;
                    case "RunFromBattleField":
                        attacker.Run(GameBase.CurrentGame.CurrentBattleField);
                        return;
                }
            }
        }

        private static void HealWithItemsOrAttacks(Enemy attacker, double[] statPercentages)
        {
            if (AutoConsume(attacker, statPercentages))
            {
                return;
            }
            else
            {
                //Change to something else?
                attacker.RecoverMana();
            }
        }

        public static void AIStyle_Healer(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            //If self is critically wounded, then just heal self
            if (AutoConsume(attacker, statPercentages_Timid1))
                return;

            List<Enemy> woundedAllies = new List<Enemy>();

            foreach (Enemy enemy in activeEnemies)
            {
                if (enemy != null && enemy.PercentStats[0] < 0.75)
                {
                    woundedAllies.Add(enemy);
                }
            }

            if (woundedAllies.Count > 0)
            {
                //Heal wounded allies
                Enemy mostWounded = woundedAllies[0];

                foreach (Enemy enemy in woundedAllies)
                {
                    if (enemy.CurrentStats[0] < mostWounded.CurrentStats[0])
                        mostWounded = enemy;
                }

                Attack attack = Attack.FindBestHealingAttack(attacker, mostWounded, GameBase.Stats.HP);

                if (attack != null)
                    attacker.DoAttack(attack, mostWounded);
                else
                    AIStyle_Timid(attacker, activePlayers);
            }
            else
                AIStyle_Timid(attacker, activePlayers);
        }

        //public static void AIStyle_HealerBerserk(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        //{
        //    List<Enemy> woundedAllies = new List<Enemy>();

        //    foreach (Enemy enemy in activeEnemies)
        //    {
        //        if (enemy != null && enemy.PercentStats[0] < 0.75)
        //        {
        //            woundedAllies.Add(enemy);
        //        }
        //    }

        //    if (woundedAllies.Count > 0)
        //    {
        //        //Heal wounded allies
        //        Enemy mostWounded = woundedAllies[0];

        //        foreach (Enemy enemy in woundedAllies)
        //        {
        //            if (enemy.CurrentStats[0] < mostWounded.CurrentStats[0])
        //                mostWounded = enemy;
        //        }

        //        Attack attack = Attack.FindBestHealingAttack(attacker, mostWounded, GameBase.Stats.HP);

        //        if (attack != null)
        //            attacker.DoAttack(attack, mostWounded);
        //        else
        //            AIStyle_Berserk(attacker, activePlayers);
        //    }
        //    else
        //        AIStyle_Berserk(attacker, activePlayers);
        //}

        //public static T FindWeakestCharacter<T>(IEnumerable<T> characters, GameBase.Stats stat) where T : Character
        //{
        //    T selected = null;
        //    foreach (T thing in characters)
        //    {
        //        if (thing != null && thing.CurrentStats[(int)stat] > selected.CurrentStats[(int)stat])
        //            selected = thing;
        //    }
        //    return selected;
        //}

        //public static T FindStrongestCharacter<T>(IEnumerable<T> characters, GameBase.Stats stat) where T : Character
        //{
        //    T selected = null;
        //    foreach (T thing in characters)
        //    {
        //        if (selected == null || thing.CurrentStats[(int)stat] < selected.CurrentStats[(int)stat])
        //            selected = thing;
        //    }
        //    return selected;
        //}

        //public static T FindSmallestDistance<T>(Point location, IEnumerable<T> targets) where T : Character
        //{

        //}

        public static List<T> CharactersWithinRange<T>(Character attacker, IEnumerable<Attack> attacks, IEnumerable<T> characters) where T : Character
        {
            int highBound = 0;

            foreach (Attack attack in attacks)
            {
                if (attack.Range > highBound)
                    highBound = attack.Range;
            }

            List<T> withinRange = new List<T>();

            foreach (T thing in characters)
            {
                if (thing != null && attacker.DirectDistanceFrom(thing) <= highBound)
                {
                    withinRange.Add(thing);
                }
            }

            return withinRange;
        }
    }
}
