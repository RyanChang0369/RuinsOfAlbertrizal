using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map : WorldMapObject
    {
        public bool SeenIntroduction { get; set; }

        public Message IntroMessage { get; set; }

        public List<Player> Players { get; set; }

        [XmlIgnore]
        public List<Player> AlivePlayers
        {
            get
            {
                List<Player> players = new List<Player>();
                
                foreach (Player player in Players)
                {
                    if (!player.IsDead)
                        players.Add(player);
                }

                return players;
            }
        }

        [XmlIgnore]
        public List<Player> DeadPlayers
        {
            get
            {
                List<Player> players = new List<Player>();

                foreach (Player player in Players)
                {
                    if (player.IsDead)
                        players.Add(player);
                }

                return players;
            }
        }

        public List<Boss> StoredBosses { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

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

        /// <summary>
        /// True if there are no moer alive players
        /// </summary>
        [XmlIgnore]
        public bool GameOver
        {
            get => AlivePlayers.Count == 0;
        }

        public Map()
        {
            Players = new List<Player>();
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
    }
}