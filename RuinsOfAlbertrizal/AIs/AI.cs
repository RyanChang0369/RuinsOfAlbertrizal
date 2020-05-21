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
            /// <summary>
            /// The player controls this character.
            /// Only works with a player character, and is treated as NoAI everywhere else
            /// </summary>
            [Description(
                "The player controls this character." +
                " Only works with a player character, and is treated as NoAI everywhere else.")]
            Player = 0,
            [Description("A special AI that does nothing. Use when you do not want to change a character's AI in a buff.")]
            NoChange = 1,
            /// <summary>
            /// No AI. Cannot Move.
            /// </summary>
            [Description("No AI. Cannot move, regenerate mana, or attack.")]
            NoAI = 2,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Moves so that its most damaging attack or attacks is in range of the player.
            /// Attacks twice if doing so will deal the most damage to the player.
            /// No regard to health.
            /// Recovers only if unable to attack.
            /// Cannot use items.
            /// </summary>
            [Description(
                "Attacks player with the most damaging attacks possible." + 
                " Moves so that its most damaging attack or attacks is in range of the player." + 
                " Attacks twice if doing so will deal the most damage to the player." +
                " No regard to health." +
                " Recovers mana only if unable to attack." +
                " Cannot use items.")]
            Beserk = 10,
            /// <summary>
            /// Attacks player with the most damaging attacks possible.
            /// Moves so that its most damaging attack or attacks is in range of the player.
            /// Attacks twice if doing so will deal the most damage to the player.
            /// Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack.
            /// Recovers only if unable to attack.
            /// Uses items to recover health.
            /// </summary>
            [Description(
                "Attacks player with the most damaging attacks possible." +
                " Moves so that its most damaging attack or attacks is in range of the player." +
                " Attacks twice if doing so will deal the most damage to the player." +
                " Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack." +
                " Recovers mana only if unable to attack." +
                " Uses items to recover health and mana.")]
            Beserk_UseItem = 11,
            /// <summary>
            /// Tries to stay above the player.
            /// Attacks player with the most damaging attacks possible.
            /// If has attacked, move so it is out of range of player.
            /// Attacks twice only if player is very far away (twice the range of the player).
            /// Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack. Only heals if out of range of player.
            /// Recovers when unable to attack and player is out of range or when player is very far away (twice the player's movement range).
            /// Uses items when unable to attack and player is out of range or when player is very far away (twice the player's movement range).
            /// </summary>
            [Description(
                "Tries to stay above the player. " +
                "Attacks player with the most damaging attacks possible. " +
                "If has attacked, move so it is out of range of player. " +
                "Attacks twice only if player is very far away (twice the range of the player). " +
                "Heals with healing items and/or spells if below 25% health or if received 50% damage in one attack. Only heals if out of range of player. " +
                "Recovers when unable to attack and player is out of range or when player is very far away (twice the player's movement range). " +
                "Uses items when unable to attack and player is out of range or when player is very far away (twice the player's movement range). " +
                "")]
            Flying = 20,
        }

        [XmlIgnore]
        public static IList<AIStyle> AIStyleNames
        {
            get
            {
                return Enum.GetValues(typeof(AIStyle)).Cast<AIStyle>().ToList<AIStyle>();
            }
        }

        //[XmlIgnore]
        //public static string[] AIStyleTooltips =
        //{
            
        //};
    }
}
