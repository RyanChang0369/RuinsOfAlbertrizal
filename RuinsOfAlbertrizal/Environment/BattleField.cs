using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField : ITurnBasedObject
    {
        public Message StoredMessage { get; set; }

        public List<Enemy> Enemies { get; set; }

        [XmlIgnore]
        public List<Enemy> AliveEnemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                foreach (Enemy enemy in Enemies)
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

                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.IsDead)
                        enemies.Add(enemy);
                }

                return enemies;
            }
        }

        public Enemy[] ActiveEnemies { get; set; }

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

            Random rnd = new Random();
            Enemies = Enemies.OrderBy(x => rnd.Next()).ToList();

            StoredMessage = new Message();
            ActiveEnemies = new Enemy[GameBase.NumActiveCharacters];

            for (int i = 0; i < Math.Min(Enemies.Count, GameBase.NumActiveCharacters); i++)
            {
                ActiveEnemies[i] = Enemies[i];
            }
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
            int totalLevel = 0;

            foreach (Player player in players)
            {
                totalLevel = player.Level;
            }

            int averageLevel = totalLevel / players.Count;

            return averageLevel + RNG.GetRandomInteger(-2, 3);
        }

        private void PlayerWins()
        {
            AwardPoints();
            AwardXP();
            //Get loot here
        }

        private void PlayerLoses()
        {

        }


        private void AwardPoints()
        {
            int points = 0;

            foreach (Enemy enemy in DeadEnemies)
            {
                points += enemy.GetPointsGained();
            }

            GameBase.CurrentGame.CurrentLevel.Points += points;
        }

        /// <summary>
        /// Gives XP for each player. Dead players recieve half the XP.
        /// </summary>
        private void AwardXP()
        {
            int totalXP = 0;

            foreach (Enemy enemy in DeadEnemies)
            {
                totalXP += enemy.GetXPGained();
            }

            foreach (Player player in GameBase.CurrentGame.Players)
            {
                if (player.IsDead)
                    player.XP += (int)Math.Round(totalXP / 2.0);
                else
                    player.XP += totalXP;
            }
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

            if (ConcurrentCharacters.Count > 0)
            {
                int fateSelector = RNG.GetRandomInteger(ConcurrentCharacters.Count);
                StartRound(ConcurrentCharacters[fateSelector]);
            }
        }

        public void StartRound(Character character)
        {
            SpeedTimer.Stop();

            if (character.GetType() == typeof(Player))
                PlayerTurn((Player)character);
            else
                EnemyTurn((Enemy)character);
        }

        private void EnemyTurn(Enemy enemy)
        {

        }

        private void PlayerTurn(Player player)
        {

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
            if (PlayerHasWon)
                PlayerWins();
        }

        public void NotifyAttackBegin(Attack attack, Character attacker)
        {
            StoredMessage.Add($"{attacker.DisplayName} attacked with {attack.DisplayName}!");
            GameBase.CurrentGame.CurrentBattleInterface.NotifyAttackBegin(attack, attacker);
        }

        public void NotifyAttackHit(Attack attack, Character target)
        {
            StoredMessage.Add($"{target.DisplayName} received {attack.StatLoss[0]} points of damage!");
            GameBase.CurrentGame.CurrentBattleInterface.NotifyAttackHit(attack, target);
        }

        public void NotifyItemUsed(Item item, Character user)
        {
            if (item.GetType() == typeof(Consumable))
                StoredMessage.Add($"{user.DisplayName} has consumed {item.DisplayName}.");
            else if (item.GetType() == typeof(Equiptment))
                StoredMessage.Add($"{user.DisplayName} has equipted {item.DisplayName}.");
            GameBase.CurrentGame.CurrentBattleInterface.NotifyItemUsed(item, user);
        }
    }
}
