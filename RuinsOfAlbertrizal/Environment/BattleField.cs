using RuinsOfAlbertrizal.AIs;
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
        [XmlIgnore]
        public BattleInterface BattleInterface { get; set; }

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
        public Player[] ActivePlayers
        {
            get => GameBase.CurrentGame.ActivePlayers;
            set => GameBase.CurrentGame.ActivePlayers = value;
        }

        [XmlIgnore]
        public List<Character> AllCharacters
        {
            get
            {
                List<Character> characters = new List<Character>();
                characters.AddRange(GameBase.CurrentGame.Players);
                characters.AddRange(Enemies);
                return characters;
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
        public int MaxSpeed
        {
            get
            {
                int max = int.MinValue;
                foreach (Character character in AliveCharacters)
                    max += character.CurrentStats[4];
                return max;
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
        /// Creates a new battlefield using the players in GameBase.CurrentGame. Navigate to BattleInterface to show the interface.
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

            BattleInterface = new BattleInterface(this);
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

            //Failsafe
            if (enemies.Count < 1)
                enemies.Add(GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies));

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
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);
                enemies.Add(enemy);
                totalEnemyBI += enemy.BattleIndex;
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

            int allEnemyBI = 0;

            foreach (Enemy enemy in GameBase.CurrentGame.CurrentLevel.StoredEnemies)
            {
                allEnemyBI += enemy.BattleIndex;
            }

            int averageBI = allEnemyBI / GameBase.CurrentGame.CurrentLevel.StoredEnemies.Count;

            while (totalEnemyBI < adjustedPlayerBI * 0.75)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);

                if (enemy.BattleIndex >= averageBI)
                {
                    enemies.Add(enemy);
                    totalEnemyBI += enemy.BattleIndex;
                }
            }

            while (totalEnemyBI < adjustedPlayerBI)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);
                enemies.Add(enemy);
                totalEnemyBI += enemy.BattleIndex;
            }

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

            int allEnemyBI = 0;

            foreach (Enemy enemy in GameBase.CurrentGame.CurrentLevel.StoredEnemies)
            {
                allEnemyBI += enemy.BattleIndex;
            }

            int averageBI = allEnemyBI / GameBase.CurrentGame.CurrentLevel.StoredEnemies.Count;

            while (totalEnemyBI < adjustedPlayerBI * 0.75)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);

                if (enemy.BattleIndex <= averageBI)
                {
                    enemies.Add(enemy);
                    totalEnemyBI += enemy.BattleIndex;
                }
            }

            while (totalEnemyBI < adjustedPlayerBI)
            {
                Enemy enemy = GetRandomEnemy(players, GameBase.CurrentGame.CurrentLevel.StoredEnemies);
                enemies.Add(enemy);
                totalEnemyBI += enemy.BattleIndex;
            }

            return enemies;
        }

        private Enemy GetRandomEnemy(List<Player> players, List<Enemy> storedEnemies)
        {
            int fateSelector = RNG.GetRandomInteger(storedEnemies.Count);
            Enemy enemy = storedEnemies[fateSelector];
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
        /// Gives XP for each player. Dead players receive half the XP.
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
            

            

            SpeedTimer = new Timer(GameBase.TickSpeed);
            SpeedTimer.Elapsed += new ElapsedEventHandler(Tick);
            SpeedTimer.Start();
        }


        private void Tick(object sender, ElapsedEventArgs e)
        {
            ElaspedTime++;

            ConcurrentCharacters.Clear();

            foreach (Character character in AliveCharacters)
            {
                character.TurnTicks += character.CurrentStats[4];
            }

            for (int i = 0; i < ConcurrentCharacters.Count; i++)
                StartRound(ConcurrentCharacters[i]);
        }

        public void StartRound(Character character)
        {
            SpeedTimer.Stop();

            character.TurnTicks = 0;

            StoredMessage.Add($"{character.DisplayName} is up.");

            if (character.GetType() == typeof(Player))
                PlayerTurn((Player)character);
            else
                EnemyTurn((Enemy)character);
        }

        private void EnemyTurn(Enemy enemy)
        {
            //Selects target twice as each character has two turns
            AI.SelectTarget(enemy, GameBase.CurrentGame.ActivePlayers, ActiveEnemies);
            AI.SelectTarget(enemy, GameBase.CurrentGame.ActivePlayers, ActiveEnemies);
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
            else if (PlayerHasLost)
                PlayerLoses();
        }

        public void NotifyAttackBegin(Attack attack, Character attacker)
        {
            StoredMessage.Add($"{attacker.DisplayName} attacked with {attack.DisplayName}!");
            BattleInterface.NotifyAttackBegin(attack, attacker);
        }

        public void NotifyAttackHit(Attack attack, Character target)
        {
            StoredMessage.Add($"{target.DisplayName} received {attack.StatLoss[0]} points of damage!");
            BattleInterface.NotifyAttackHit(attack, target);
        }

        public void NotifyItemUsed(Item item, Character user)
        {
            if (item.GetType() == typeof(Consumable))
                StoredMessage.Add($"{user.DisplayName} has consumed {item.DisplayName}.");
            else if (item.GetType() == typeof(Equiptment))
                StoredMessage.Add($"{user.DisplayName} has equipted {item.DisplayName}.");
            BattleInterface.NotifyItemUsed(item, user);
        }
    }
}
