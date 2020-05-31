using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField : ITurnBasedObject
    {
        public List<string> StoredMessages { get; set; }

        public List<Enemy> Enemies { get; set; }

        [XmlIgnore]
        public List<Enemy> AliveEnemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                foreach (Enemy enemy in enemies)
                {
                    if (!enemy.IsDead)
                        enemies.Add(enemy);
                }

                return enemies;
            }
        }

        [XmlIgnore]
        public List<Enemy> DeadEnemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.IsDead)
                        enemies.Add(enemy);
                }

                return enemies;
            }
        }

        [XmlIgnore]
        public List<Enemy> ActiveEnemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (i > GameBase.NumActiveCharacters - 1)
                        break;

                    enemies.Add(Enemies[i]);
                }

                return enemies;
            }
        }

        [XmlIgnore]
        public List<Character> AliveCharacters
        {
            get
            {
                List<Character> characters = new List<Character>();
                characters.AddRange(GameBase.CurrentGame.AlivePlayers);
                characters.AddRange(AliveEnemies);
                return characters;
            }
        }

        [XmlIgnore]
        public List<Character> DeadCharacters
        {
            get
            {
                List<Character> characters = new List<Character>();
                characters.AddRange(GameBase.CurrentGame.DeadPlayers);
                characters.AddRange(DeadEnemies);
                return characters;
            }
        }

        [XmlIgnore]
        public bool PlayerHasWon
        {
            get => AliveEnemies.Count < 1;
        }

        [XmlIgnore]
        public bool PlayerHasLost
        {
            get => GameBase.CurrentGame.GameOver;
        }

        /// <summary>
        /// The characters with the same speed in queue to have their round started.
        /// </summary>
        public List<Character> ConcurrentCharacters { get; set; }

        /// <summary>
        /// The number of rounds that has passed since the beginning of this map.
        /// </summary>
        public int RoundsPassed { get; set; }

        public int ElaspedTime { get; set; }

        private Timer SpeedTimer { get; set; }

        /// <summary>
        /// Creates a new battlefield using the players in GameBase.CurrentGame
        /// </summary>
        public BattleField()
        {
            Enemies = SummonEnemies(GameBase.CurrentGame.Players);
        }

        private List<Enemy> SummonEnemies(List<Player> players)
        {
            List<Enemy> enemies = new List<Enemy>();
            int totalPlayerBI = 0;
            int totalEnemyBI = 0;

            foreach (Player player in players)
            {
                totalPlayerBI += player.BattleIndex;
            }

            int fateSelector = RNG.GetRandomInteger(3);

            switch (fateSelector)
            {
                case 0:
                    enemies = SummonEnemiesPath1(players, totalEnemyBI, (int)(totalPlayerBI * GameBase.CurrentGame.TotalDifficulty));
                    break;
                case 1:
                    enemies = SummonEnemiesPath2(players, totalEnemyBI, (int)(totalPlayerBI * GameBase.CurrentGame.TotalDifficulty));
                    break;
                case 2:
                    enemies = SummonEnemiesPath3(players, totalEnemyBI, (int)(totalPlayerBI * GameBase.CurrentGame.TotalDifficulty));
                    break;
            }

            return enemies;
        }

        /// <summary>
        /// Summon enemies at random
        /// </summary>
        /// <param name="players"></param>
        /// <param name="totalEnemyBI"></param>
        /// <param name="adjustedPlayerBI"></param>
        /// <returns></returns>
        private List<Enemy> SummonEnemiesPath1(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {
            List<Enemy> enemies = new List<Enemy>();

            while (totalEnemyBI < adjustedPlayerBI)
            {
                enemies.Add(GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies));
            }

            return enemies;
        }

        /// <summary>
        /// Prefer a few stronger enemies
        /// </summary>
        /// <param name="players"></param>
        /// <param name="totalEnemyBI"></param>
        /// <param name="adjustedPlayerBI"></param>
        /// <returns></returns>
        private List<Enemy> SummonEnemiesPath2(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {
            List<Enemy> enemies = new List<Enemy>();

            int averageBI = totalEnemyBI / GameBase.CurrentGame.CurrentLevel.StoredEnemies.Count;

            while (totalEnemyBI < adjustedPlayerBI * 0.75)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);

                if (enemy.BattleIndex > averageBI)
                {
                    enemies.Add(enemy);
                }
            }

            while (totalEnemyBI < adjustedPlayerBI * 0.25)
                enemies.Add(GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies));

            return enemies;
        }

        /// <summary>
        /// Prefer many weaker enemies
        /// </summary>
        /// <param name="players"></param>
        /// <param name="totalEnemyBI"></param>
        /// <param name="adjustedPlayerBI"></param>
        /// <returns></returns>
        private List<Enemy> SummonEnemiesPath3(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {
            List<Enemy> enemies = new List<Enemy>();

            int averageBI = totalEnemyBI / GameBase.CurrentGame.CurrentLevel.StoredEnemies.Count;

            while (totalEnemyBI < adjustedPlayerBI * 0.75)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);

                if (enemy.BattleIndex < averageBI)
                {
                    enemies.Add(enemy);
                }
            }

            while (totalEnemyBI < adjustedPlayerBI * 0.25)
                enemies.Add(GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies));

            return enemies;
        }

        private Enemy GetRandomEnemy(List<Player> players, List<Enemy> enemies)
        {
            int fateSelector = RNG.GetRandomInteger(enemies.Count);
            Enemy enemy = enemies[fateSelector].DeepClone();
            enemy.Level = GetAdjustedLevel(players);
            return enemy;
        }

        private int GetAdjustedLevel(List<Player> players)
        {
            int averageLevel = 0;
            int totalLevel = 0;

            foreach (Player player in players)
            {
                totalLevel = player.Level;
            }

            averageLevel = totalLevel / players.Count;

            return averageLevel + RNG.GetRandomInteger(-2, 3);
        }

        public bool IsTargetable(Character target, Attack attack)
        {
            if (target.IsInvunerable())
                return false;

            bool ignoreDeathCheck = false;

            //Dead characters are only targetable if the attack can revive them.
            foreach (Buff buff in attack.Buffs)
            {
                if (buff.TypeOfBuff == Buff.BuffType.Revive)
                    ignoreDeathCheck = true;
            }

            if (!ignoreDeathCheck && target.IsDead)
                return false;

            return true;
        }

        private void AwardPoints()
        {
            int points = 0;

            foreach (Enemy enemy in DeadEnemies)
            {
                points += (int)Math.Round(enemy.BattleIndex / 10.0);
            }

            GameBase.CurrentGame.CurrentLevel.Points += points;
        }

        public void SetTimer()
        {
            int fastestSpeed = 0;

            ConcurrentCharacters.Clear();

            foreach (Character character in AliveCharacters)
            {
                if (character.CurrentStats[4] >= fastestSpeed)
                    fastestSpeed = character.CurrentStats[4];
            }

            foreach (Character character in AliveCharacters)
            {
                if (character.CurrentStats[4] == fastestSpeed)
                    ConcurrentCharacters.Add(character);
            }

            SpeedTimer = new Timer(GameBase.TickSpeed);
            SpeedTimer.Elapsed += new ElapsedEventHandler(Tick);
            SpeedTimer.Start();
        }


        private void Tick(object sender, ElapsedEventArgs e)
        {
            ElaspedTime++;

            int fateSelector;

            if (ConcurrentCharacters.Count > 0)
            {
                fateSelector = RNG.GetRandomInteger(ConcurrentCharacters.Count);
                StartRound(ConcurrentCharacters[fateSelector]);
            }
        }

        public void StartRound(Character character)
        {
            SpeedTimer.Stop();
        }

        /// <summary>
        /// Hand control over to the next character
        /// </summary>
        public void EndRound()
        {
            RoundsPassed++;
            SetTimer();
        }

        public void StartTurn()
        {
            //RemoveDeadCharacters();
        }

        public void EndTurn()
        {
            //RemoveDeadCharacters();
        }
    }
}
