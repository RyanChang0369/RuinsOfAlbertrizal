using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Map : WorldMapObject
    {
        public Timer SpeedTimer { get; set; }

        public bool SeenIntroduction { get; set; }

        public Message IntroMessage { get; set; }

        public List<Player> StoredPlayers { get; set; }

        public List<Boss> StoredBosses { get; set; }

        public List<Enemy> StoredEnemies { get; set; }

        public List<Player> AlivePlayers { get; set; }

        public List<Boss> AliveBosses { get; set; }

        public List<Enemy> AliveEnemies { get; set; }

        /// <summary>
        /// The players with the same speed in queue to have their round started.
        /// </summary>
        public List<Player> ConcurrentPlayers { get; set; }

        /// <summary>
        /// The bosses with the same speed in queue to have their round started.
        /// </summary>
        public List<Boss> ConcurrentBosses { get; set; }

        /// <summary>
        /// The enemies with the same speed in queue to have their round started.
        /// </summary>
        public List<Enemy> ConcurrentEnemies { get; set; }

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
        /// The number of rounds that has passed since the beginning of this map.
        /// </summary>
        public int RoundsPassed { get; set; }

        public Map()
        {
            StoredPlayers = new List<Player>();
            StoredEnemies = new List<Enemy>();
            StoredBosses = new List<Boss>();
            ConcurrentPlayers = new List<Player>();
            ConcurrentEnemies = new List<Enemy>();
            ConcurrentBosses = new List<Boss>();
            AlivePlayers = new List<Player>();
            AliveEnemies = new List<Enemy>();
            AliveBosses = new List<Boss>();
            StoredBuffs = new List<Buff>();
            StoredAttacks = new List<Attack>();
            StoredItems = new List<Item>();
            StoredEquiptments = new List<Equiptment>();
            StoredConsumables = new List<Consumable>();
            StoredHazards = new List<Hazard>();
            StoredBlocks = new List<Block>();
            Levels = new List<Level>();
        }

        /// <summary>
        /// Makes all the characters alive
        /// </summary>
        public void SpawnAll()
        {
            AlivePlayers = StoredPlayers;
            AliveEnemies = StoredEnemies;
            AliveBosses = StoredBosses;
        }

        public void SetTimer()
        {
            int fastestSpeed = 0;

            foreach (Player player in AlivePlayers)
            {
                if (player.CurrentStats[4] > fastestSpeed)
                    fastestSpeed = player.CurrentStats[4];
            }

            foreach (Boss boss in AliveBosses)
            {
                if (boss.CurrentStats[4] > fastestSpeed)
                    fastestSpeed = boss.CurrentStats[4];
            }

            foreach (Enemy enemy in AliveEnemies)
            {
                if (enemy.CurrentStats[4] > fastestSpeed)
                    fastestSpeed = enemy.CurrentStats[4];
            }

            SpeedTimer = new Timer();
        }

        public void StartRound()
        {
            SpeedTimer.Stop();
        }

        /// <summary>
        /// Hand control over to the next character
        /// </summary>
        public void EndRound()
        {
            RoundsPassed++;
            SpeedTimer.Start();
        }

        public void StartTurn()
        {
            throw new NotImplementedException();
        }
    }
}