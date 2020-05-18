using RuinsOfAlbertrizal.Characters;
using System.Collections.Generic;
using System.Timers;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField : ITurnBasedObject
    {
        public List<Character> AliveCharacters { get; set; }

        public List<Character> DeadCharacters { get; set; }

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
            DeadCharacters = new List<Character>();

            AliveCharacters.AddRange(players);
            AliveCharacters.AddRange(bosses);
            AliveCharacters.AddRange(enemies);
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
            RemoveDeadCharacters();
        }

        public void EndTurn()
        {
            RemoveDeadCharacters();
        }

        private void RemoveDeadCharacters()
        {
            foreach (Character character in AliveCharacters)
            {
                if (character.IsDead)
                {
                    AliveCharacters.Remove(character);
                    DeadCharacters.Add(character);
                }
            }
        }
    }
}
