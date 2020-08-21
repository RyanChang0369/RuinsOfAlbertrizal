using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class BattleField : ObjectOfAlbertrizal, IRoundBasedObject
    {
        public const int BattleFieldWidth = 20;

        public const int BattleFieldHeight = 4;

        [XmlIgnore]
        public int CalculatedX { get; set; }

        [XmlIgnore]
        public int CalculatedY { get; set; }

        [XmlIgnore]
        public BattleInterface BattleInterface { get; set; }

        public Message StoredMessage { get; set; }

        public List<Enemy> Enemies { get; set; }

        public int TurnNum { get; set; }

        /// <summary>
        /// The index of the player in ActivePlayers that is having a turn.
        /// </summary>
        public int SelectedPlayerIndex { get; set; }

        /// <summary>
        /// The player in ActivePlayers that is having a turn
        /// </summary>
        [XmlIgnore]
        public Player SelectedPlayer
        {
            get
            {
                try
                {
                    return ActivePlayers[SelectedPlayerIndex];
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        [XmlIgnore]
        public Attack SelectedAttack { get; set; }

        [XmlIgnore]
        public Character SelectedTarget { get; set; }

        //[XmlIgnore]
        //public List<Character> SelectedSide { get; set; }

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
        public List<Player> Players
        {
            get => GameBase.CurrentGame.Players;
            set => GameBase.CurrentGame.Players = value;
        }

        [XmlIgnore]
        public Player[] ActivePlayers => GameBase.CurrentGame.ActivePlayers;

        [XmlIgnore]
        public List<Player> AlivePlayers => GameBase.CurrentGame.AlivePlayers;

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
        public List<Character> ActiveCharacters
        {
            get
            {
                List<Character> characters = new List<Character>();
                for (int i = 0; i < GameBase.NumActiveCharacters; i++)
                {
                    if (ActivePlayers[i] != null)
                        characters.Add(ActivePlayers[i]);
                    if (ActiveEnemies[i] != null)
                        characters.Add(ActiveEnemies[i]);
                }
                return characters;
            }
        }

        [XmlIgnore]
        public List<Character> AliveCharacters
        {
            get
            {
                List<Character> characters = new List<Character>();
                characters.AddRange(AlivePlayers);
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
                int max = 0;
                foreach (Character character in AliveCharacters)
                    max += character.CurrentStats[4];

                OnPropertyChanged("MaxSpeed");
                OnPropertyChanged("MaxTicks");

                return max;
            }
        }

        [XmlIgnore]
        public int MaxTicks => MaxSpeed * 5;

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

        private System.Threading.Timer SpeedTimer { get; set; }

        /// <summary>
        /// Creates a new battlefield using the players in GameBase.CurrentGame. Navigate to BattleInterface to show the interface.
        /// </summary>
        public BattleField()
        {
            SelectedPlayerIndex = -1;

            ConcurrentCharacters = new List<Character>();

            Enemies = SummonEnemies(GameBase.CurrentGame.Players);

            Random rnd = new Random();
            Enemies = Enemies.OrderBy(x => rnd.Next()).ToList();

            StoredMessage = new Message();
            ActiveEnemies = new Enemy[GameBase.NumActiveCharacters];

            for (int i = 0; i < Math.Min(Enemies.Count, GameBase.NumActiveCharacters); i++)
            {
                ActiveEnemies[i] = Enemies[i];
            }

            System.Windows.MessageBox.Show(StringStorage.GetEnemyEncounterString(Enemies.Count));

            foreach (Player player in ActivePlayers)
            {
                if (player != null)
                    player.LoadImage();
            }

            foreach (Enemy enemy in ActiveEnemies)
            {
                if (enemy != null)
                    enemy.LoadImage();
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
                enemies.Add(GetRandomEnemy(players));

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
                Enemy enemy = GetRandomEnemy(players);
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
                Enemy enemy = GetRandomEnemy(players);

                if (enemy.BattleIndex >= averageBI)
                {
                    enemies.Add(enemy);
                    totalEnemyBI += enemy.BattleIndex;
                }
            }

            while (totalEnemyBI < adjustedPlayerBI)
            {
                Enemy enemy = GetRandomEnemy(players);
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
                Enemy enemy = GetRandomEnemy(players);

                if (enemy.BattleIndex <= averageBI)
                {
                    enemies.Add(enemy);
                    totalEnemyBI += enemy.BattleIndex;
                }
            }

            while (totalEnemyBI < adjustedPlayerBI)
            {
                Enemy enemy = GetRandomEnemy(players);
                enemies.Add(enemy);
                totalEnemyBI += enemy.BattleIndex;
            }

            return enemies;
        }

        private Enemy GetRandomEnemy(List<Player> players)
        {
            List<Enemy> storedEnemies = GameBase.CurrentGame.StoredEnemies;

            int fateSelector = RNG.GetRandomInteger(storedEnemies.Count);
            Enemy enemy = storedEnemies[fateSelector].RoAMemoryClone();
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

            int selectedLevel = averageLevel + RNG.GetRandomInteger(-2, 3);

            if (selectedLevel < 1)
                selectedLevel = 1;

            return selectedLevel;
        }

        private void PlayerWins()
        {
            AwardPoints();
            AwardXP();
            //Get loot here

            BattleEnd();
        }

        private void PlayerLoses()
        {
            throw new NotImplementedException();
        }

        public void PlayerRunsAway()
        {
            StoredMessage.Add("You ran away!");
            BattleEnd();
        }

        private void BattleEnd()
        {
            StoredMessage.Add("Exiting battle...");
            Thread.Sleep(1738);
            BattleInterface.Exit();
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
            SpeedTimer = new System.Threading.Timer(Tick, null, 3000, GameBase.TickSpeed);
        }


        private void Tick(object sender)
        {
            //Avoid conflicting ticks by stopping timer during a tick
            SpeedTimer.Change(Timeout.Infinite, Timeout.Infinite);

            if (ConcurrentCharacters.Count > 0)
            {
                //If there are characters waiting to have a turn, "cancel" the tick
                //and instead do the turn. This is to avoid having the AI wail on the
                //player before they even get to select an attack
                StartCharacterRound(ConcurrentCharacters[0]);
                ConcurrentCharacters.RemoveAt(0);
            }
            else
            {
                //Else, do the turn as normal.
                ElaspedTime++;


                foreach (Character character in ActiveCharacters)
                {
                    character.TurnTicks += character.CurrentStats[4];
                }

                foreach (Character character in ActiveCharacters)
                {
                    if (character.TurnTicks >= MaxTicks)
                        ConcurrentCharacters.Add(character);
                }

                BattleInterface.NotifyTick();

                if (ConcurrentCharacters.Count > 0)
                {
                    StartCharacterRound(ConcurrentCharacters[0]);
                    ConcurrentCharacters.RemoveAt(0);
                }
                else
                    SpeedTimer.Change(GameBase.TickSpeed, GameBase.TickSpeed);
            }
        }

        public void StartCharacterRound(Character character)
        {
            RoundKeeper.RoundStart(this);
            RoundKeeper.RoundStart(character);
            RoundKeeper.RoundStart(character.CurrentConsumables);
            RoundKeeper.RoundStart(character.AllAttacks);

            StoredMessage.Add($"{character.DisplayName} is up.");

            Console.WriteLine(ElaspedTime);

            if (character.GetType() == typeof(Player))
                NewPlayerTurn((Player)character);
            else
                NewEnemyTurn((Enemy)character);
        }

        public void StartCharacterTurn(Character character)
        {
            RoundKeeper.TurnStart(this);
            RoundKeeper.TurnStart(character);
            RoundKeeper.TurnStart(character.CurrentConsumables);
            RoundKeeper.TurnStart(character.AllAttacks);
        }

        public void EndCharacterRound(Character character)
        {
            RoundKeeper.RoundEnd(character);
            RoundKeeper.RoundEnd(character.CurrentConsumables);
            RoundKeeper.RoundEnd(character.AllAttacks);
            RoundKeeper.RoundEnd(this);
        }

        public void EndCharacterTurn(Character character)
        {
            RoundKeeper.TurnEnd(character);
            RoundKeeper.TurnEnd(character.CurrentConsumables);
            RoundKeeper.TurnEnd(character.AllAttacks);
            RoundKeeper.TurnEnd(this);
        }

        /// <summary>
        /// Ends character's turn and starts another turn if character still has turns left.
        /// </summary>
        /// <param name="character"></param>
        public void NewTurn(Character character)
        {
            EndCharacterTurn(character);

            if (character.GetType() == typeof(Player))
            {
                NewPlayerTurn((Player)character);
            }
            else
            {
                NewEnemyTurn((Enemy)character);
            }
        }

        private async void NewEnemyTurn(Enemy enemy)
        {
            if (TurnNum < GameBase.NumTurns)
            {
                StartCharacterTurn(enemy);
                BattleInterface.NotifyEnemyRoundStart();
                await MiscMethods.TaskDelay(500);
                AI.SelectTarget(enemy, GameBase.CurrentGame.ActivePlayers, ActiveEnemies);
            }
            else
            {
                EndCharacterRound(enemy);
            }
        }

        private void NewPlayerTurn(Player player)
        {
            SelectedPlayerIndex = Array.IndexOf(ActivePlayers, player);

            if (TurnNum < GameBase.NumTurns)
            {
                StartCharacterTurn(player);
                BattleInterface.NotifyPlayerRoundStart();
            }
            else
            {
                EndCharacterRound(player);
            }
        }

        public void StartRound()
        {

        }

        public void EndRound()
        {
            SelectedPlayerIndex = -1;
            SelectedAttack = null;
            SelectedTarget = null;
            TurnNum = 0;
            RoundsPassed++;
            SpeedTimer.Change(GameBase.TickSpeed, GameBase.TickSpeed);
        }

        public void StartTurn()
        {
            
        }

        public void EndTurn()
        {
            TurnNum++;

            if (PlayerHasWon)
                PlayerWins();
            else if (PlayerHasLost)
                PlayerLoses();
        }

        /// <summary>
        /// Notifies BattleField that an attack has begun.
        /// </summary>
        /// <param name="attack">The selected attack</param>
        /// <param name="attacker">The character doing the attack</param>
        /// <param name="charging">True if the attack is being charged</param>
        public void NotifyAttackBegin(Attack attack, Character attacker, Character target, bool charging)
        {
            if (charging)
            {
                StoredMessage.Add($"{attacker.DisplayName} is charging attack {attack.DisplayName}");
            }
            else
            {
                StoredMessage.Add($"{attacker.DisplayName} attacked with {attack.DisplayName}!");
                attacker.TurnsSinceLastAttack = 0;
            }

            BattleInterface.NotifyAttackBegin(attack, attacker, target, charging);

            NewTurn(attacker);
        }

        public void NotifyAttackHit(Attack attack, Character target, List<string> appendedLines)
        {
            StoredMessage.Add($"{target.DisplayName} received {attack.StatLoss[0]} points of damage!");

            foreach (string line in appendedLines)
            {
                StoredMessage.Add(line);
            }

            BattleInterface.NotifyAttackHit(attack, target);
        }

        public void NotifyAttackMiss(Attack attack, Character target)
        {
            StoredMessage.Add($"{attack.Name} missed!");
            BattleInterface.NotifyAttackHit(attack, target);
        }

        /// <summary>
        /// Allows for the previewing of movement without actually moving.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="oldLocation"></param>
        public async void FakeMovement(Character character, Point oldLocation)
        {
            await BattleInterface.NotifyMovement(character, oldLocation);
        }

        public async void NotifyMovement(Character character, Point oldLocation)
        {
            await BattleInterface.NotifyMovement(character, oldLocation);
            if (character.MovementPoints <= 0)
            {
                NewTurn(character);
            }
        }

        /// <summary>
        /// Same as NotifyMovement, but this will always end the character's turn.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="oldLocation"></param>
        public async void FinalizeMovement(Character character, Point oldLocation)
        {
            await BattleInterface.NotifyMovement(character, oldLocation);
            NewTurn(character);
        }

        public void NotifyItemUsed(Item item, Character user)
        {
            if (item.GetType() == typeof(Consumable))
                StoredMessage.Add($"{user.DisplayName} has consumed {item.DisplayName}.");
            else if (item.GetType() == typeof(Equiptment))
                StoredMessage.Add($"{user.DisplayName} has equipted {item.DisplayName}.");
            BattleInterface.NotifyItemUsed(item, user);
        }

        public void NotifyDeath(Character character)
        {
            StoredMessage.Add($"{character.Name} was slain!");

            if (character.GetType() == typeof(Player))
            {
                throw new NotImplementedException();
            }
            else
            {
                SwapEnemy((Enemy)character, Enemies.Except(ActiveEnemies).GetRandomValue());
            }

            BattleInterface.NotifyDeath(character);
        }

        public void NotifyPlayerLost()
        {
            foreach (Player player in Players)
            {
                player.AppliedStats = new int[GameBase.NumStats];
                player.AppliedStats[0] = (int)Math.Round(player.CurrentStats[0] * -0.9);
            }

            BattleInterface.NotifyPlayerLost();
        }

        public static int GetDistance(Point a, Point b)
        {
            int count = 0;
            Point newA = a;

            while (newA.X != b.X && newA.Y != b.Y)
            {
                if (newA.X > b.X)
                {
                    newA.X--;
                }
                else if (newA.X < b.Y)
                {
                    newA.X++;
                }
                else if (newA.Y > b.Y)
                {
                    newA.Y--;
                }
                else
                {
                    newA.Y++;
                }
                count++;
            }

            return count;
        }

        private void SwapEnemy(Enemy oldEnemy, Enemy newEnemy)
        {
            int index = Array.IndexOf(ActiveEnemies, oldEnemy);

            if (index == -1)
                throw new ArgumentOutOfRangeException($"{oldEnemy.DisplayName} not found within ActiveEnemies");
            
            ActiveEnemies[index] = newEnemy;
            oldEnemy.UnloadImage();
            newEnemy.LoadImage();
            BattleInterface.SwapEnemy(index);
        }

        private void SwapPlayer(Player oldPlayer, Player newPlayer)
        {
            int index = Array.IndexOf(GameBase.CurrentGame.ActivePlayerGuids, oldPlayer.GlobalID);

            if (index == -1)
                throw new ArgumentOutOfRangeException($"{oldPlayer.DisplayName} not found within ActivePlayers");

            GameBase.CurrentGame.ActivePlayerGuids[index] = newPlayer.GlobalID;
            oldPlayer.UnloadImage();
            newPlayer.LoadImage();
            BattleInterface.SwapPlayer(index);
        }
    }
}