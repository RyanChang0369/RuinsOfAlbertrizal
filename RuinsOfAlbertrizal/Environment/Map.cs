using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map : ObjectOfAlbertrizal
    {
        public List<string> IntroText { get; set; }

        public List<Player> StoredPlayers { get; set; }

        /// <summary>
        /// For reference only
        /// </summary>
        public List<Enemy> StoredEnemies { get; set; }

        /// <summary>
        /// For reference only
        /// </summary>
        public List<Boss> StoredBosses { get; set; }

        public List<Buff> StoredBuffs { get; set; }

        public List<Attack> StoredAttacks { get; set; }

        public List<Item> StoredItems { get; set; }

        public List<Equiptment> StoredEquiptment { get; set; }

        public List<Consumable> StoredConsumables { get; set; }

        public List<Hazard> StoredHazards { get; set; }

        public List<Level> Levels { get; set; }

        public int LevelsCompleted { get; set; }

        [XmlIgnore]
        public Level CurrentLevel { get => Levels[LevelsCompleted]; }

        public int TurnsPassed { get; set; }

        public Map()
        {
            StoredEnemies = new List<Enemy>();
            StoredBosses = new List<Boss>();
            StoredBuffs = new List<Buff>();
            StoredAttacks = new List<Attack>();
            StoredItems = new List<Item>();
            StoredEquiptment = new List<Equiptment>();
            StoredConsumables = new List<Consumable>();
            StoredHazards = new List<Hazard>();
            Levels = new List<Level>();
        }
    }
}