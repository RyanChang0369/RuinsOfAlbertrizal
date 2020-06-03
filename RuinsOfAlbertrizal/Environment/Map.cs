using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map : WorldMapObject, INotifyPropertyChanged
    {
        private List<Item> playerItems;
        private List<Consumable> playerConsumables;
        private List<Equiptment> playerEquiptments;

        public bool PlayerCreated { get; set; }

        public bool AllowForPlayerCreation { get; set; }

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

        /// <summary>
        /// The players that can attack.
        /// </summary>
        public List<Player> ActivePlayers { get; set; }

        public List<Boss> StoredBosses { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

        public List<Buff> StoredBuffs { get; set; }

        public List<Attack> StoredAttacks { get; set; }

        public List<Item> StoredItems { get; set; }

        public List<Equiptment> StoredEquiptments { get; set; }

        public List<Consumable> StoredConsumables { get; set; }

        public List<Hazard> StoredHazards { get; set; }

        public List<Block> StoredBlocks { get; set; }

        public List<Item> PlayerItems
        {
            get => playerItems;
            set
            {
                playerItems = value;
                OnPropertyChanged();
            }
        }

        public List<Consumable> PlayerConsumables
        {
            get => playerConsumables;
            set
            {
                playerConsumables = value;
                OnPropertyChanged();
            }
        }

        public List<Equiptment> PlayerEquiptments
        {
            get => playerEquiptments;
            set
            {
                playerEquiptments = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The higher this number is, the more difficult the enemy encounters will be. Ranges from 0.0 to infinity.
        /// Set to 1.0 for "completely" fair gameplay.
        /// </summary>
        public double Difficulty { get; set; }

        /// <summary>
        /// The total difficulty equal to the map's difficulty times the level's
        /// </summary>
        [XmlIgnore]
        public double TotalDifficulty
        {
            get => Difficulty * CurrentLevel.Difficulty;
        }

        public BattleField BattleField { get; set; }

        public List<Level> Levels { get; set; }

        public int LevelsCompleted { get; set; }

        [XmlIgnore]
        public Level CurrentLevel { get => Levels[LevelsCompleted]; }

        /// <summary>
        /// True if there are no more alive players
        /// </summary>
        [XmlIgnore]
        public bool GameOver
        {
            get => AlivePlayers.Count == 0;
        }

        public Map()
        {
            AllowForPlayerCreation = true;
            Difficulty = 1.0;
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
            PlayerItems = new List<Item>();
            PlayerConsumables = new List<Consumable>();
            PlayerEquiptments = new List<Equiptment>();
        }
    }
}