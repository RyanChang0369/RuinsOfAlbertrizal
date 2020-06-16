using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for BattleInterface.xaml
    /// </summary>
    public partial class BattleInterface : BasePage
    {
        public BattleField BattleField { get; set; }

        private List<Image> playerImages = new List<Image>();

        private List<Image> enemyImages = new List<Image>();

        private List<Image> playerTargetImages = new List<Image>();

        private List<Image> enemyTargetImages = new List<Image>();

        public BattleInterface(BattleField battleField)
        {
            InitializeComponent();
            ActionPanel.Visibility = Visibility.Hidden;
            BattleField = battleField;
            DataContext = BattleField;
            BackgroundImg.Source = GameBase.CurrentGame.CurrentLevel.WorldImgAsBitmapSource;
            UpdateImageLists();
            UpdateGrid();
            InitialAnimation();

            //For animation help: http://www.codescratcher.com/wpf/sliding-panel-in-wpf/
        }

        private void UpdateImageLists()
        {
            playerImages = new List<Image>
            {
                player0, player1, player2, player3
            };

            enemyImages = new List<Image>
            {
                enemy0, enemy1, enemy2, enemy3
            };

            playerTargetImages = new List<Image>
            {
                targetPlayer0, targetPlayer1, targetPlayer2, targetPlayer3
            };

            enemyTargetImages = new List<Image>
            {
                targetEnemy0, targetEnemy1, targetEnemy2, targetEnemy3
            };
        }

        private void UpdateGrid()
        {
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                UpdatePlayerImage(i);
                UpdateEnemyImage(i);
            }
        }

        private void UpdatePlayerImage(int i)
        {
            try
            {
                if (GameBase.CurrentGame.ActivePlayers[i].WorldImgIsValid)
                    playerImages[i].Source = GameBase.CurrentGame.ActivePlayers[i].WorldImgAsBitmapSource;
                else
                    playerImages[i].Source = new BitmapImage();

                playerImages[i].Tag = GameBase.CurrentGame.ActivePlayers[i];
                playerTargetImages[i].Tag = GameBase.CurrentGame.ActivePlayers[i];
            }
            catch (Exception)
            {

            }
        }

        private void UpdateEnemyImage(int i)
        {
            try
            {
                if (BattleField.Enemies[i].WorldImgIsValid)
                    enemyImages[i].Source = BattleField.Enemies[i].WorldImgAsBitmapSource;
                else
                    enemyImages[i].Source = new BitmapImage();

                enemyImages[i].Tag = BattleField.ActiveEnemies[i];
                enemyTargetImages[i].Tag = BattleField.ActiveEnemies[i];
            }
            catch (Exception)
            {

            }
        }

        public void DoAttack(Character attacker, List<Character> targets)
        {

        }

        private void SwapEnemy(Enemy oldEnemy, Enemy newEnemy)
        {
            int index = Array.IndexOf(BattleField.ActiveEnemies, oldEnemy);

            if (index == -1)
                throw new ArgumentOutOfRangeException($"{oldEnemy.DisplayName} not found within ActiveEnemies");

            Animate("enemySlideOut", enemyImages[index]);
            BattleField.ActiveEnemies[index] = newEnemy;
            UpdateEnemyImage(index);
            Animate("enemySlideIn", enemyImages[index]);
        }

        private void SwapPlayer(Player oldPlayer, Player newPlayer)
        {
            int index = Array.IndexOf(GameBase.CurrentGame.ActivePlayerGuids, oldPlayer.GlobalID);

            if (index == -1)
                throw new ArgumentOutOfRangeException($"{oldPlayer.DisplayName} not found within ActivePlayers");

            Animate("playerSlideOut", playerImages[index]);
            GameBase.CurrentGame.ActivePlayerGuids[index] = newPlayer.GlobalID;
            UpdatePlayerImage(index);
            Animate("playerSlideIn", playerImages[index]);
        }

        private void PreparePlayerAttack(Player attacker, Attack selectedAttack)
        {
            ToggleTargets(attacker, selectedAttack);
        }

        /// <summary>
        /// Targets or untargets enemies
        /// </summary>
        /// <param name="attack">The attack selected</param>
        /// <param name="hideTargets">If true, then hide targets instead of showing targets</param>
        private void ToggleTargets(Character attacker, Attack attack, bool hideTargets = false)
        {
            List<int>[] targetIndexes = attack.GetAttackIndexes(attacker, GameBase.CurrentGame.ActivePlayers, BattleField.ActiveEnemies);

            List<int> playerTargetIndexes = targetIndexes[0];
            List<int> enemyTargetIndexes = targetIndexes[1];

            if (hideTargets)
                HideTargets(playerTargetIndexes, enemyTargetIndexes);
            else
                ShowTargets(playerTargetIndexes, enemyTargetIndexes);
        }

        /// <summary>
        /// Fades in the target symbols.
        /// </summary>
        /// <param name="playerTargetIndexes">The indexes of targetable players in ActivePlayers</param>
        /// <param name="enemyTargetIndexes">The indexes of targetable enemies in ActiveEnemies</param>
        private void ShowTargets(List<int> playerTargetIndexes, List<int> enemyTargetIndexes)
        {
            foreach (int i in playerTargetIndexes)
            {
                Animate("targetFadeIn", playerTargetImages[i]);
            }

            foreach (int i in enemyTargetIndexes)
            {
                Animate("targetFadeIn", enemyTargetImages[i]);
            }
        }

        /// <summary>
        /// Fades out the target symbols.
        /// </summary>
        /// <param name="playerTargetIndexes">The indexes of targetable players in ActivePlayers</param>
        /// <param name="enemyTargetIndexes">The indexes of targetable enemies in ActiveEnemies</param>
        private void HideTargets(List<int> playerTargetIndexes, List<int> enemyTargetIndexes)
        {
            foreach (int i in playerTargetIndexes)
            {
                Animate("targetFadeOut", playerTargetImages[i]);
            }

            foreach (int i in enemyTargetIndexes)
            {
                Animate("targetFadeOut", enemyTargetImages[i]);
            }
        }

        private void InitialAnimation()
        {
            foreach (Image image in playerImages)
            {
                Animate("playerSlideIn", image);
                Thread.Sleep(50);
            }

            foreach (Image image in enemyImages)
            {
                Animate("enemySlideIn", image);
                Thread.Sleep(50);
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Content = "Select Attack";
            



            btn.Content = "Attack";
        }

        private void LoadAttackSelector()
        {
            
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            FloatingInventory inventory = new FloatingInventory(BattleField.SelectedPlayer, BattleField.TurnNum);
        }

        private void Flee_Click(object sender, RoutedEventArgs e)
        {
            Player.Run(BattleField);
        }

        public void Exit()
        {
            Navigate("[back]");
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            BattleField.SetTimer();
        }

        public void NotifyAttackBegin(Attack attack, Character attacker)
        {
            //Do animation
        }

        public void NotifyAttackHit(Attack attack, Character target)
        {
            //Do animation
        }

        public void NotifyItemUsed(Item item, Character user)
        {
            //update sprites
        }

        public void NotifyPlayerIsReady(Player player, int turnNum)
        {
            Dispatcher.Invoke(() =>
            {
                ActionPanel.Visibility = Visibility.Visible;
            });
        }

        public void NotifyPlayerAttacking(Player player, int turnNum)
        {
            Dispatcher.Invoke(() =>
            {
                ActionPanel.Visibility = Visibility.Hidden;
            });
        }

        public void NotifyTick()
        {
            
        }

        private void BuffIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Control ctrl = (Control)sender;
            BuffDisplayer displayer = new BuffDisplayer((List<Buff>)ctrl.Tag);
            displayer.Show();
        }
    }
}
