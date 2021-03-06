﻿using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        public List<Player> AlivePlayers => Players.FindAll(player => !player.IsDead);

        [XmlIgnore]
        public List<Player> DeadPlayers => Players.FindAll(player => player.IsDead);

        public Guid[] ActivePlayerGuids { get; set; }

        /// <summary>
        /// The players that can attack. Consider using ActivePlayerGuids for better performance.
        /// </summary>
        [XmlIgnore]
        public Player[] ActivePlayers => Players.ToArray().FilterByGlobalID(ActivePlayerGuids);

        public List<Boss> StoredBosses { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

        public List<Buff> StoredBuffs { get; set; }

        public List<Attack> StoredAttacks { get; set; }

        public List<Item> StoredItems { get; set; }

        public List<Equiptment> StoredEquiptments { get; set; }

        public List<Consumable> StoredConsumables { get; set; }

        /// <summary>
        /// What each player starts out with.
        /// </summary>
        [XmlIgnore]
        public List<Item> DefaultItems
        {
            get
            {
                return StoredItems.FindAll(Item.IsDefault);
            }
        }

        /// <summary>
        /// What each player starts out with.
        /// </summary>
        [XmlIgnore]
        public List<Equiptment> DefaultEquiptments
        {
            get
            {
                return StoredEquiptments.FindAll(Item.IsDefault);
            }
        }

        /// <summary>
        /// What each player starts out with.
        /// </summary>
        [XmlIgnore]
        public List<Consumable> DefaultConsumables
        {
            get
            {
                return StoredConsumables.FindAll(Item.IsDefault);
            }
        }

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

        public override void Load(Map map)
        {
            foreach (Attack attack in StoredAttacks)
            {
                attack.Load(map);
            }

            foreach (Consumable consumable in StoredConsumables)
            {
                consumable.Load(map);
            }

            foreach (Equiptment equiptment in StoredEquiptments)
            {
                equiptment.Load(map);
            }

            foreach (Player player in Players)
            {
                player.Load(map);
            }

            foreach (Enemy enemy in StoredEnemies)
            {
                enemy.Load(map);
            }

            foreach (Level level in Levels)
            {
                level.Load(map);
            }

            //if (map == GameBase.StaticGame)
            //{
            //    GameBase.StaticGame = FileHandler.LoadMap(GameBase.StaticMapLocation);
            //}
            //else
            //{
            //    GameBase.CurrentGame = FileHandler.LoadMap(GameBase.CurrentMapLocation);
            //}
        }

        public override void Unload(bool force)
        {
            foreach (Attack attack in StoredAttacks)
            {
                attack.Unload(force);
            }

            foreach (Consumable consumable in StoredConsumables)
            {
                consumable.Unload(force);
            }

            foreach (Equiptment equiptment in StoredEquiptments)
            {
                equiptment.Unload(force);
            }


            foreach (Player player in Players)
            {
                player.Unload(force);
            }

            foreach (Enemy enemy in StoredEnemies)
            {
                enemy.Unload(force);
            }

            foreach (Level level in Levels)
            {
                level.Unload(force);
            }
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

        /// <summary>
        /// Obtains and clones an item.
        /// </summary>
        /// <param name="item"></param>
        public void PlayerObtainObject(Item item)
        {
            item = item.RoAMemoryClone();

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
            ImagePrompt prompt = new ImagePrompt("You found an item!", StringStorage.GetItemFindString(item), item.IconAsBitmapSource, "Keep", "Discard");

            prompt.ShowDialog();

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
            ImagePrompt prompt = new ImagePrompt("You found an equiptment!", StringStorage.GetItemFindString(equiptment), equiptment.IconAsBitmapSource, "Keep", "Discard");

            prompt.ShowDialog();

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
            ImagePrompt prompt = new ImagePrompt("You found a consumable!", StringStorage.GetItemFindString(consumable), consumable.IconAsBitmapSource, "Keep", "Discard");

            prompt.ShowDialog();

            if ((bool)prompt.DialogResult)
            {
                GameBase.CurrentGame.PlayerConsumables.Add(consumable);
            }
        }

        public void FindTeamMember(Enemy teamMember)
        {
            ImagePrompt prompt = new ImagePrompt("You found a team member!",
                StringStorage.GetTeamMemberFindString(teamMember),
                teamMember.IconAsBitmapSource, "Accept", "Refuse");

            prompt.ShowDialog();

            if ((bool)prompt.DialogResult)
            {
                Players.Add(new Player(teamMember)); 
            }
        }
    }
}