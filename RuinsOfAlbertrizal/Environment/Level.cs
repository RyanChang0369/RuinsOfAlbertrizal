using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Level
    {
        /// <summary>
        /// The Enemies that can appear in this level.
        /// </summary>
        public List<Enemy> Enemies { get; set; }

        /// <summary>
        /// Number of enemies to kill before boss fight. Set to -1 for unwinnable
        /// </summary>
        public int EnemiesToKill { get; set; }

        /// <summary>
        /// The boss that appears at the end of the level.
        /// </summary>
        public Boss Boss { get; set; }

        /// <summary>
        /// The win condition.
        /// </summary>
        public int WinCondition { get; set; }

        public enum WinConditions
        {
            None,
            DefeatEnemies
        }

        public Level(List<Enemy> enemies, Boss boss, int winCondition, int enemiesToKill)
        {
            Enemies = enemies;
            Boss = boss;
            WinCondition = winCondition;
            EnemiesToKill = enemiesToKill;
        }
    }
}
