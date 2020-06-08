using RuinsOfAlbertrizal.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Validates a player created map for issues.
    /// </summary>
    public static class GameValidator
    {
        public static void Validate(Map map)
        {
            if (map.Players.Count < 1)
                AlertUser("You have not created any players. If the end user fails to create a player, the game will crash under most circumstances.");
            if (map.Levels.Count < 1)
                AlertUser("You have not created any levels.");
            
            foreach (Level level in map.Levels)
            {
                if (level.StoredEnemies.Count < 1)
                    AlertUser($"The level titled {level.Name} does not have any stored enemies.");

                if (level.Bosses.Count < 1)
                    AlertUser($"The level titled {level.Name} does not have any stored bosses.");
            }

        }

        public static void AlertUser(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
