using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map : WorldMapObject
    {
        public bool SeenIntroduction { get; set; }

        public Message IntroMessage { get; set; }

        public List<Player> StoredPlayers { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

        public List<Boss> StoredBosses { get; set; }

        public List<Buff> StoredBuffs { get; set; }

        public List<Attack> StoredAttacks { get; set; }

        public List<Item> StoredItems { get; set; }

        public List<Equiptment> StoredEquiptments { get; set; }

        public List<Consumable> StoredConsumables { get; set; }

        public List<Hazard> StoredHazards { get; set; }

        public List<Block> StoredBlocks { get; set; }

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
            StoredEquiptments = new List<Equiptment>();
            StoredConsumables = new List<Consumable>();
            StoredHazards = new List<Hazard>();
            StoredBlocks = new List<Block>();
            Levels = new List<Level>();
        }

        //public override void Reset()
        //{
        //    SeenIntroduction = false;
        //    IntroMessage.Reset();
        //    LevelsCompleted = 0;

        //    for (int i = 0; i < Levels.Count; i++)
        //    {
        //        Levels[i].Reset();
        //    }
        //}
    }
}