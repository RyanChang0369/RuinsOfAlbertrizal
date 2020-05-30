using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System.Collections.Generic;
using System.Timers;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField : ITurnBasedObject
    {
        public List<Enemy> Enemies { get; set; }

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

        //public BattleField(List<Player> players, List<Boss> bosses, List<Enemy> enemies)
        //{
        //    ConcurrentCharacters = new List<Character>();
        //    DeadCharacters = new List<Character>();
        //}

        /// <summary>
        /// Creates a new battlefield using the players in GameBase.CurrentGame
        /// </summary>
        public BattleField()
        {

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

            while (totalEnemyBI < totalPlayerBI * GameBase.CurrentGame.TotalDifficulty)
            {

            }
        }

        private List<Enemy> SummonEnemiesPath1(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {

        }

        private List<Enemy> SummonEnemiesPath2(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {

        }

        private List<Enemy> SummonEnemiesPath3(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {

        }

        private List<Enemy> SummonEnemiesPath4(List<Player> players, int totalEnemyBI, int adjustedPlayerBI)
        {

        }

        private bool IsTargetable(Character target, Attack attack)
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
