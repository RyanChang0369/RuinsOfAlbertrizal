using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Level
    {
        string Name { get; set; }

        Bitmap BackgroundImage { get; set; }
        /// <summary>
        /// The Enemies that can appear in this level.
        /// </summary>
        public List<Enemy> Enemies { get; set; }

        /// <summary>
        /// Boss fight starts at next encounter if points is equal to or exceeds this.
        /// </summary>
        public double MaxPoints { get; set; }

        public double Points { get; set; }

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

        [XmlIgnore]
        public bool HasWon {
            get
            {
                switch (WinCondition)
                {
                    case (int)WinConditions.None:
                        return false;
                    case (int)WinConditions.DefeatEnemies:
                        return Boss.IsDead;
                    default:
                        return false;
                }
            }
        }

        public Level(string name, Bitmap backgroundImage,
            List<Enemy> enemies, Boss boss, int winCondition, double maxPoints)
        {
            Name = name;
            BackgroundImage = backgroundImage;
            Enemies = enemies;
            Boss = boss;
            WinCondition = winCondition;
            MaxPoints = maxPoints;
            Points = 0;
        }
    }
}
