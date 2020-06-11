using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.AIs
{
    public class AI
    {
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
                //" Moves so that its most damaging attack or attacks is in range of the player." + 
                " Attacks twice if doing so will deal the most damage to the player." +
                " No regard to health." +
                " Recovers mana only if unable to attack." +
                " Cannot use items.")]
            Berserk = 10,
            [Description(
                "Attacks player with the most damaging attacks possible." +
                //" Moves so that its most damaging attack or attacks is in range of the player." +
                " Attacks twice if doing so will deal the most damage to the player." +
                " Heals with healing items and/or spells if below 40% health." +
                " Recovers mana only if unable to attack." +
                " Uses items to recover health and mana.")]
            Berserk_UseItem = 11,
            [Description(
                "Similar to beserk. " +
                "Heals with healing items and/or spells if below 60% health.")]
            Timid = 20,
            [Description(
                "Similar to Timid. " +
                "Heals other enemies if they are below 75% health. Identical to Timid otherwise.")]
            Healer = 30,
            [Description(
            //"Tries to stay above the player. " +
            "Same as timid."
            //"Moves so it is out of range of player. Moves on its first turn. " +
            //"Attacks twice only if player is very far away (twice the range of the player). " +
            //"Only heals if out of range of player. " +
            //"Recovers when unable to attack and player is out of range or when player is very far away (twice the player's movement range). " +
            //"Uses items when unable to attack and player is out of range or when player is very far away (twice the player's movement range). "
            )]
            Flying = 40,
            [Description("Same as healer.")]
            Flying_Healer = 41
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
                case AIStyle.Flying:
                    AIStyle_Timid(attacker, activePlayers);
                    break;
                case AIStyle.Healer:
                case AIStyle.Flying_Healer:
                    AIStyle_Healer(attacker, activePlayers, activeEnemies);
                    break;
            }
        }

        public static void UseItem(Enemy user, GameBase.Stats stat)
        {
            if (user.InventoryConsumables.Count < 1)
                return;

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

            //If recovering mana is better, just recover mana
            if (selectedConsumable != null && stat == GameBase.Stats.Mana && (int)Math.Round(user.LeveledStats[1] * 0.3) > selectedConsumable.GetLifetimeStatGain(user, GameBase.Stats.Mana))
                user.RecoverMana();
            //Else consume the item if it is not null
            else if (selectedConsumable != null)
                user.Consume(selectedConsumable);
            //Else recover
            else
                user.RecoverMana();
        }

        public static void AIStyle_Berserk(Enemy attacker, Player[] activePlayers)
        {
            //Find player with lowest hp and select that as target
            Player target = activePlayers[0];

            foreach (Player player in activePlayers)
            {
                if (player.CurrentStats[0] < target.CurrentStats[0])
                    target = player;
            }

            double fateSelector = RNG.GetRandomDouble();

            Attack attack;

            if (attacker.GetMultiTargetAttacks().Count < 1)
            {
                //Attacker does not multitarget attacks
                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP);
            }
            else if (fateSelector < 0.5 && target.PercentStats[0] < 0.35)
            {
                //50% chance that the enemy will use the strongest multitargeting attack instead of the strongest single attack
                //if target is below 35% health

                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP,
                    attacker.GetMultiTargetAttacks());
            }
            else if (fateSelector < 0.1)
            {
                //10% chance that the enemy will use the strongest multitargeting attack anyways

                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP,
                    attacker.GetMultiTargetAttacks());
            }
            else
            {
                //Attack weakest player with strongest attack
                attack = Attack.FindStrongestAttack(attacker, target, GameBase.Stats.HP);
            }

            //If cannot use attack due to mana, recover
            if (attack.StatCostToUser[1] > attacker.CurrentStats[1])
            {
                attacker.RecoverMana();
                return;
            }

            attacker.Attack(attack, target);
        }

        public static void AIStyle_BerserkUseItem(Enemy attacker, Player[] activePlayers)
        {
            if (attacker.PercentStats[0] < 0.4)
                UseItem(attacker, GameBase.Stats.HP);
            else
                AIStyle_Berserk(attacker, activePlayers);
        }

        public static void AIStyle_Timid(Enemy attacker, Player[] activePlayers)
        {
            if (attacker.PercentStats[0] < 0.6)
                UseItem(attacker, GameBase.Stats.HP);
            else if (attacker.PercentStats[2] < 0.3)
                UseItem(attacker, GameBase.Stats.Def);
            else if (attacker.PercentStats[4] < 0.4)
                UseItem(attacker, GameBase.Stats.Spd);
            else if (attacker.PercentStats[5] < 0.25)
                UseItem(attacker, GameBase.Stats.Int);
            else if (attacker.PercentStats[3] < 0.25)
                UseItem(attacker, GameBase.Stats.Dmg);
            else
                AIStyle_Berserk(attacker, activePlayers);
        }

        public static void AIStyle_Healer(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            List<Enemy> woundedAllies = new List<Enemy>();

            //If self is critically wounded, then just heal self
            if (attacker.PercentStats[0] < 0.2)
                UseItem(attacker, GameBase.Stats.HP);

            foreach (Enemy enemy in activeEnemies)
            {
                if (enemy.PercentStats[0] < 0.75)
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
                attacker.Attack(attack, mostWounded);
            }
            else
                AIStyle_Timid(attacker, activePlayers);
        }
    }
}
