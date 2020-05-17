using RuinsOfAlbertrizal.Characters;
using System.Collections.Generic;
using System.Timers;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField
    {
        public List<Player> AlivePlayers { get; set; }

        public List<Boss> AliveBosses { get; set; }

        public List<Enemy> AliveEnemies { get; set; }

        public List<Player> DeadPlayers { get; set; }

        public List<Boss> DeadBosses { get; set; }

        public List<Enemy> DeadEnemies { get; set; }

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

        public BattleField(List<Player> players, List<Boss> bosses, List<Enemy> enemies)
        {
            ConcurrentCharacters = new List<Character>();
            DeadPlayers = new List<Player>();
            DeadEnemies = new List<Enemy>();
            DeadBosses = new List<Boss>();

            AlivePlayers = players;
            AliveBosses = bosses;
            AliveEnemies = enemies;
        }

        public void SetTimer()
        {
            int fastestSpeed = 0;

            ConcurrentCharacters.Clear();

            foreach (Player player in AlivePlayers)
            {
                if (player.CurrentStats[4] >= fastestSpeed)
                    fastestSpeed = player.CurrentStats[4];
            }

            foreach (Boss boss in AliveBosses)
            {
                if (boss.CurrentStats[4] >= fastestSpeed)
                    fastestSpeed = boss.CurrentStats[4];
            }

            foreach (Enemy enemy in AliveEnemies)
            {
                if (enemy.CurrentStats[4] >= fastestSpeed)
                    fastestSpeed = enemy.CurrentStats[4];
            }

            foreach (Player player in AlivePlayers)
            {
                if (player.CurrentStats[4] == fastestSpeed)
                    ConcurrentCharacters.Add(player);
            }

            foreach (Boss boss in AliveBosses)
            {
                if (boss.CurrentStats[4] == fastestSpeed)
                    ConcurrentCharacters.Add(boss);
            }

            foreach (Enemy enemy in AliveEnemies)
            {
                if (enemy.CurrentStats[4] == fastestSpeed)
                    ConcurrentCharacters.Add(enemy);
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
            
        }
    }
}
