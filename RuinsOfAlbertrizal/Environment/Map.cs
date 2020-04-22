using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map
    {
        //public int EditorTabPosition { get; set; }

        public string Name { get; set; }
        public List<string> IntroText { get; set; }

        public Player Player { get; set; }

        /// <summary>
        /// For reference only
        /// </summary>
        public List<Enemy> StoredEnemies { get; set; }

        [XmlIgnore]
        public string[] StoredEnemyNames
        {
            get
            {
                string[] names = new string[StoredEnemies.Count];

                try
                {
                    for (int i = 0; i < names.Length; i++)
                        names[i] = StoredEnemies[i].SpecificName;
                }
                catch (Exception)
                {
                    return null;
                }

                return names;
            }
        }

        /// <summary>
        /// For reference only
        /// </summary>
        public List<Boss> StoredBosses { get; set; }

        [XmlIgnore]
        public string[] StoredBossNames
        {
            get
            {
                string[] names = new string[StoredEnemies.Count];

                try
                {
                    for (int i = 0; i < names.Length; i++)
                        names[i] = StoredBosses[i].SpecificName;
                }
                catch (Exception)
                {
                    return null;
                }

                return names;
            }
        }

        public List<Level> Levels { get; set; }

        public int LevelsCompleted { get; set; }

        [XmlIgnore]
        public Level CurrentLevel { get => Levels[LevelsCompleted]; }

        public Map()
        {
            StoredEnemies = new List<Enemy>();
            StoredBosses = new List<Boss>();
            Levels = new List<Level>();
        }

        //public Map(List<string> introText, Player player, List<Level> levels)
        //{
        //    IntroText = introText;
        //    Player = player;
        //    Levels = levels;
        //    LevelsCompleted = 0;
        //}
    }
}