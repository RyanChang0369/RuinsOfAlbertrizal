using RuinsOfAlbertrizal.Characters;
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
                "Heals with healing items and/or spells if below 50% health or if received 20% damage in one attack")]
            Timid = 20,
            [Description(
                "Similar to Timid. " +
                "Heals other enemies if they are below 75% health. Identical to Timid otherwise.")]
            Healer = 30,
            [Description(
            //"Tries to stay above the player. " +
            "Similar to Timid, but hovers above the ground (visual difference)."
            //"Moves so it is out of range of player. Moves on its first turn. " +
            //"Attacks twice only if player is very far away (twice the range of the player). " +
            //"Only heals if out of range of player. " +
            //"Recovers when unable to attack and player is out of range or when player is very far away (twice the player's movement range). " +
            //"Uses items when unable to attack and player is out of range or when player is very far away (twice the player's movement range). "
            )]
            Flying = 40,
            [Description("Similar to Healer, but hovers above the ground (visual difference).")]
            Flying_Healer = 41
        }

        [XmlIgnore]
        public static IList<AIStyle> AIStyleNames
        {
            get
            {
                return Enum.GetValues(typeof(AIStyle)).Cast<AIStyle>().ToList<AIStyle>();
            }
        }

        /// <summary>
        /// Selects a target and attacks it.
        /// </summary>
        /// <param name="aiStyle"></param>
        /// <param name="attacker"></param>
        /// <param name="activePlayers"></param>
        /// <param name="activeEnemies"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static void SelectTarget(AIStyle aiStyle, Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            switch (aiStyle)
            {
                case AIStyle.NoAI:
                case AIStyle.NoChange:
                case AIStyle.Player:
                    throw new ArgumentException("The current AIStyle forbids attacking");
                case AIStyle.Berserk:
                    AIStyle_Berserk(attacker, activePlayers, activeEnemies);
                    break;
                case AIStyle.Berserk_UseItem:

                    break;
            }
        }

        public static void AIStyle_Berserk(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            //Select weapon and attack here

            //Find player with lowest hp and select that as target
            Player target = activePlayers[0];

            foreach (Player player in activePlayers)
            {
                if (player.CurrentStats[0] < target.CurrentStats[0])
                    target = player;
            }

            double fateSelector = RNG.GetRandomDouble();

            Attack attack;

            if (fateSelector < 0.5 && target.PercentStats[0] < 0.35)
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

            attacker.Attack(attack, target);
        }

        public static void AIStyle_BerserkUseItem(Enemy attacker, Player[] activePlayers, Enemy[] activeEnemies)
        {
            
        }
    }
}
