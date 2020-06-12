using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public Guid[] ActivePlayerGuids { get; set; }

        /// <summary>
        /// The players that can attack. Consider using ActivePlayerGuids for better performance.
        /// </summary>
        [XmlIgnore]
        public Player[] ActivePlayers
        {
            get
            {
                return Players.ToArray().FilterByGuid(ActivePlayerGuids);
            }
        }

        public List<Boss> StoredBosses { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

        public List<Buff> StoredBuffs { get; set; }

        public List<Attack> StoredAttacks { get; set; }

        public List<Item> StoredItems { get; set; }

        public List<Equiptment> StoredEquiptments { get; set; }

        public List<Consumable> StoredConsumables { get; set; }

        /// <summary>
        /// A list of all stored items, consumables, and equipment
        /// </summary>
        [XmlIgnore]
        public List<Item> AllItems
        {
            get
            {
                List<Item> allItems = new List<Item>();

                try
                {
                    allItems.AddRange(GameBase.CurrentGame.StoredItems);

                }
                catch (ArgumentNullException)
                {

                }
                try
                {
                    allItems.AddRange(GameBase.CurrentGame.StoredEquiptments);
                }
                catch (ArgumentNullException)
                {

                }
                try
                {
                    allItems.AddRange(GameBase.CurrentGame.StoredConsumables);
                }
                catch (ArgumentNullException)
                {

                }

                return allItems;
            }
        }

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

        public BattleField CurrentBattleField { get; set; }

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
            ActivePlayerGuids = new Guid[GameBase.NumActiveCharacters];
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

        public void RefreshAllLevels()
        {
            foreach (Level level in Levels)
                level.RefreshStoredItems();
        }

        public void NextLevel()
        {
            LevelsCompleted++;

            if (LevelsCompleted >= Levels.Count)
                WinGame();
        }

        public void WinGame()
        {
            throw new NotImplementedException();
        }

        public void PlayerObtainObject(Item item)
        {
            if (item.GetType() == typeof(Equiptment))
                ObtainEquiptment((Equiptment)item);
            else if (item.GetType() == typeof(Consumable))
                ObtainConsumable((Consumable)item);
            else
                ObtainItem(item);
        }

        /// <summary>
        /// Prompts the user to keep or discard the item.
        /// </summary>
        /// <param name="item"></param>
        public void ObtainItem(Item item)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an item!", GetItemFindMessage(item), item, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerItems.Add(item);
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the equiptment.
        /// </summary>
        /// <param name="equiptment"></param>
        public void ObtainEquiptment(Equiptment equiptment)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found an equiptment!", GetItemFindMessage(equiptment), equiptment, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerEquiptments.Add(equiptment);
            }
        }

        /// <summary>
        /// Prompts the user to keep or discard the consumable.
        /// </summary>
        /// <param name="consumable"></param>
        public void ObtainConsumable(Consumable consumable)
        {
            IconedObjectPrompt prompt = new IconedObjectPrompt("You found a consumable!", GetItemFindMessage(consumable), consumable, "Keep", "Discard");

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerConsumables.Add(consumable);
            }
        }

        private string GetItemFindMessage(Item item)
        {
            return $"Out of the corner of your eye, you spot a {item.Name}.";
        }
    }
}