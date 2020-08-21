using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Text
{
    /// <summary>
    /// Stores strings that will be randomly chosen to display to the player.
    /// </summary>
    public static class StringStorage
    {
        private static string GetRandomString(string[] strings)
        {
            return strings[RNG.GetRandomInteger(strings.Length)];
        }

        public static string GetEnemyEncounterString(int numberEnemies)
        {
            string[] enemyEncounter =
            {
                $"Oh no! You were ambushed by {numberEnemies} enemies!"
            };

            return GetRandomString(enemyEncounter);
        }

        public static string GetItemFindString(Item item)
        {
            string[] itemFind =
            {
                $"Out of the corner of your eye, you spot a {item.Name}!"
            };

            return GetRandomString(itemFind);
        }

        public static string GetTeamMemberFindString(Enemy teamMember)
        {
            string[] teamMemberFind =
            {
                $"You found a new team member! {teamMember.Name} would like to join your party.",
                $"It's a {teamMember.Name}! It seems to want to join your party."
            };
            return GetRandomString(teamMemberFind);
        }
    }
}
