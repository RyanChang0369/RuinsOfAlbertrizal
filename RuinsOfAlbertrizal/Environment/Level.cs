using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Text;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RuinsOfAlbertrizal.Items;
using System.ComponentModel.DataAnnotations;

namespace RuinsOfAlbertrizal.Environment
{
    public class Level : WorldMapObject
    {
        public bool SeenIntroduction { get; set; }

        public Message IntroMessage { get; set; }

        /// <summary>
        /// The enemies that can appear in this level.
        /// </summary>
        public List<Enemy> StoredEnemies { get; set; }

        /// <summary>
        /// This number will be multiplied by the difficulty set for the map to determine the difficulty for the level.
        /// Set to 1.0 for "completely" fair gameplay.
        /// </summary>
        public double Difficulty { get; set; }

        /// <summary>
        /// Boss fight starts at next encounter if points is equal to or exceeds this.
        /// </summary>
        public int MaxPoints { get; set; }

        public int Points { get; set; }

        /// <summary>
        /// The boss(es) that appears at the end of the level. If there are multiple, they will appear at the same time.
        /// </summary>
        public List<Boss> Bosses { get; set; }

        /// <summary>
        /// The win condition.
        /// </summary>
        public WinCondition TheWinCondition { get; set; }

        public enum WinCondition
        {
            [Display(Name="Cannot Win", Description = "There's no possible way to win this level.")]
            Unwinnable,
            [Display(Name ="Boss Fight", Description = "Defeat enemies until a boss spawns, then defeat boss to win.")]
            DefeatEnemies
        }

        [XmlIgnore]
        public bool HasWon {
            get
            {
                switch (TheWinCondition)
                {
                    case WinCondition.Unwinnable:
                        return false;
                    case WinCondition.DefeatEnemies:
                        foreach (Boss boss in Bosses)
                        {
                            if (!boss.IsDead)
                                return false;
                        }
                        return true;
                    default:
                        return false;
                }
            }
        }

        public Level()
        {
            Bosses = new List<Boss>();
            StoredEnemies = new List<Enemy>();
            IntroMessage = new Message();
            Difficulty = 1.0;
        }

        /// <summary>
        /// Run whenever an enemy encounter is finished.
        /// </summary>
        public void EncounterFinished()
        {
            //foreach(Message message in Messages)
            //{
            //    if (message.ReadyToDisplay)
            //    {
            //        message.Display();
            //    }
            //}
        }
    }
}
