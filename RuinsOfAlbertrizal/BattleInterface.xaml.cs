using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for BattleInterface.xaml
    /// </summary>
    public partial class BattleInterface : BasePage
    {
        public int BattleFieldGridRowHeight => (int)BattleFieldGrid.ActualHeight / BattleField.BattleFieldHeight;

        public int BattleFieldGridColumnWidth => 200;

        public BattleField BattleField { get; set; }

        private List<Image> playerImages = new List<Image>();

        private List<Image> enemyImages = new List<Image>();

        private List<Image> playerTargetImages = new List<Image>();

        private List<Image> enemyTargetImages = new List<Image>();

        public BattleInterface(BattleField battleField)
        {
            BattleField = battleField;
            InitialPositionCharacters();
            InitializeComponent();
            ActionPanel.Visibility = Visibility.Hidden;
            DataContext = BattleField;
            BackgroundImg.Source = GameBase.CurrentGame.CurrentLevel.WorldImgAsBitmapSource;
            CreateGrid();
            UpdateImageLists();
            UpdateGrid();
            
            InitialAnimation();


            //For animation help: http://www.codescratcher.com/wpf/sliding-panel-in-wpf/
        }

        private void BattleInterface_Loaded(object sender, RoutedEventArgs e)
        {
            BattleFieldScrollViewer.ScrollToHorizontalOffset(BattleFieldScrollViewer.ScrollableWidth / 2);
            BattleField.SetTimer();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < BattleField.BattleFieldWidth; i++)
            {
                BattleFieldGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(BattleFieldGridColumnWidth) });
            }

            for (int i = 0; i < BattleField.BattleFieldHeight; i++)
            {
                BattleFieldGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }
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

        private void InitialPositionCharacters()
        {
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                if (BattleField.ActivePlayers[i] != null)
                {
                    BattleField.ActivePlayers[i].BattleFieldLocation = new System.Drawing.Point(7, i);
                }

                if (BattleField.ActiveEnemies[i] != null)
                {
                    BattleField.ActiveEnemies[i].BattleFieldLocation = new System.Drawing.Point(12, i);
                }
            }
        }

        private void UpdateGrid()
        {
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                UpdatePlayerImage(i);
                UpdateEnemyImage(i);
                UpdateEnemyLocation(i);
                UpdatePlayerLocation(i);
            }
        }

        private void UpdatePlayerImage(int i)
        {
            try
            {
                playerImages[i].GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
            catch (Exception)
            {

            }
        }

        private void UpdateEnemyImage(int i)
        {
            try
            {
                enemyImages[i].GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
            catch (Exception)
            {

            }
        }

        private void UpdateCharacterLocation(int i, Type characterType)
        {
            if (characterType == typeof(Player))
            {
                UpdatePlayerLocation(i);
            }
            else
            {
                UpdateEnemyLocation(i);
            }
        }

        private void UpdatePlayerLocation(int i)
        {
            try
            {
                playerImages[i].GetBindingExpression(Grid.RowProperty).UpdateTarget();
                playerImages[i].GetBindingExpression(Grid.ColumnProperty).UpdateTarget();
                playerTargetImages[i].GetBindingExpression(Grid.RowProperty).UpdateTarget();
                playerTargetImages[i].GetBindingExpression(Grid.ColumnProperty).UpdateTarget();
            }
            catch (Exception)
            {

            }
        }

        private void UpdateEnemyLocation(int i)
        {
            try
            {
                enemyImages[i].GetBindingExpression(Grid.RowProperty).UpdateTarget();
                enemyImages[i].GetBindingExpression(Grid.ColumnProperty).UpdateTarget();
                enemyTargetImages[i].GetBindingExpression(Grid.RowProperty).UpdateTarget();
                enemyTargetImages[i].GetBindingExpression(Grid.ColumnProperty).UpdateTarget();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SwapEnemy(Enemy oldEnemy, Enemy newEnemy)
        {
            int index = Array.IndexOf(BattleField.ActiveEnemies, oldEnemy);

            if (index == -1)
                throw new ArgumentOutOfRangeException($"{oldEnemy.DisplayName} not found within ActiveEnemies");

            Animate("enemySlideOut", enemyImages[index]);
            BattleField.ActiveEnemies[index] = newEnemy;
            oldEnemy.UnloadImage();
            newEnemy.LoadImage();
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
            oldPlayer.UnloadImage();
            newPlayer.LoadImage();
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

            List<int> attackablePlayers = targetIndexes[1];
            List<int> attackableEnemies = targetIndexes[0];

            if (hideTargets)
                HideTargets(attackablePlayers, attackableEnemies);
            else
                ShowTargets(attackablePlayers, attackableEnemies);
        }

        /// <summary>
        /// Fades in the target symbols.
        /// </summary>
        /// <param name="attackablePlayers">The indexes of targetable players in ActivePlayers</param>
        /// <param name="attackableEnemies">The indexes of targetable enemies in ActiveEnemies</param>
        private void ShowTargets(List<int> attackablePlayers, List<int> attackableEnemies)
        {
            foreach (int i in attackablePlayers)
            {
                if (BattleField.ActivePlayers[i] != null)
                {
                    playerTargetImages[i].Visibility = Visibility.Visible;
                    Animate("targetFadeIn", playerTargetImages[i]);
                }
            }

            foreach (int i in attackableEnemies)
            {
                if (BattleField.ActiveEnemies[i] != null)
                {
                    enemyTargetImages[i].Visibility = Visibility.Visible;
                    Animate("targetFadeIn", enemyTargetImages[i]); 
                }
            }
        }

        /// <summary>
        /// Fades out the target symbols.
        /// </summary>
        /// <param name="attackablePlayers">The indexes of targetable players in ActivePlayers</param>
        /// <param name="attackableEnemies">The indexes of targetable enemies in ActiveEnemies</param>
        private void HideTargets(List<int> attackablePlayers, List<int> attackableEnemies)
        {
            foreach (int i in attackablePlayers)
            {
                if (playerTargetImages[i].Tag != null)
                {
                    Animate("targetFadeOut", playerTargetImages[i]);
                    playerTargetImages[i].Visibility = Visibility.Collapsed; 
                }
            }

            foreach (int i in attackableEnemies)
            {
                if (enemyTargetImages[i].Tag != null)
                {
                    Animate("targetFadeOut", enemyTargetImages[i]);
                    enemyTargetImages[i].Visibility = Visibility.Collapsed; 
                }
            }
        }

        private async void InitialAnimation()
        {
            //Animate("overlaySlideRight", OverlayPanel1);
            //Animate("overlaySlideLeft", OverlayPanel2);
            
            //await MiscMethods.TaskDelay(2000);
            //Overlay.Visibility = Visibility.Collapsed;

            foreach (Image image in playerImages)
            {
                await MiscMethods.TaskDelay(50);
                Animate("playerSlideIn", image);
                image.Visibility = Visibility.Visible;
            }

            foreach (Image image in enemyImages)
            {
                await MiscMethods.TaskDelay(50);
                Animate("enemySlideIn", image);
                image.Visibility = Visibility.Visible;
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (BattleField.SelectedAttack == null)
                SelectAttack(btn);
            else if (!BattleField.SelectedAttack.IsCharged())
                DoCharge(btn, BattleField.SelectedPlayer, BattleField.SelectedAttack);
            else if (BattleField.SelectedTarget != null)
                DoAttack(btn, BattleField.SelectedPlayer, BattleField.SelectedTarget, BattleField.SelectedAttack);
        }

        private void AttackSelector_MouseUp(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            PreparePlayerAttack(BattleField.SelectedPlayer, (Attack)listBox.SelectedItem);

            BattleField.SelectedAttack = (Attack)listBox.SelectedItem;

            //Check if charged/cooled down
            if (BattleField.SelectedAttack.IsCharged() && BattleField.SelectedAttack.IsCooledDown())
            {
                AttackBtn.Content = "Attack!";
                AttackBtn.IsEnabled = true;
            }
            else if (!BattleField.SelectedAttack.IsCooledDown())
            {
                AttackBtn.Content = "Attack is Cooling Down";
                AttackBtn.IsEnabled = false;
            }
            else
            {
                AttackBtn.Content = "Charge Attack";
                AttackBtn.IsEnabled = true;
            }
        }

        private void SelectAttack(Button attackBtn)
        {
            attackBtn.IsEnabled = false;
            AttackPanel.Visibility = Visibility.Visible;
            ForceItemsControlUpdate(AttackSelector);
        }

        private void TargetImage_MouseUp(object sender, RoutedEventArgs e)
        {
            Image img = (Image)sender;

            if (img.Opacity != 0.9)
            {
                try
                {
                    BattleField.SelectedTarget = (Character)img.Tag;
                }
                catch (InvalidCastException)
                {
                    //BattleField.SelectedSide = (List<Character>)img.Tag;
                    throw;
                }

                Animate("targetConfirm", img);
                AttackBtn.IsEnabled = true;
            }
            else
            {
                BattleField.SelectedTarget = null;
                Animate("targetDeconfirm", img);
                AttackBtn.IsEnabled = false;
            }

            foreach (Image image in playerImages)
            {
                if (image.Opacity == 0.9 && image != img)
                {
                    Animate("targetDeconfirm", image);
                }
            }

            foreach (Image image in enemyImages)
            {
                if (image.Opacity == 0.9 && image != img)
                {
                    Animate("targetDeconfirm", image);
                }
            }
        }

        private void DoAttack(Button attackBtn, Player selectedPlayer, Character target, Attack selectedAttack)
        {
            //Hide panel
            AttackPanel.Visibility = Visibility.Collapsed;
            attackBtn.Content = "Select Attack";

            selectedPlayer.Attack(selectedAttack, target);

            ToggleTargets(selectedPlayer, selectedAttack, true);
            BattleField.SelectedAttack = null;
        }

        //private void DoAttack(Button attackBtn, Player selectedPlayer, List<Character> targets, Attack selectedAttack)
        //{
        //    //Hide panel
        //    AttackPanel.Visibility = Visibility.Collapsed;
        //    attackBtn.Content = "Select Attack";

        //    selectedPlayer.Attack(selectedAttack, targets);

        //    ToggleTargets(selectedPlayer, selectedAttack, true);
        //    BattleField.SelectedAttack = null;
        //}

        private void DoCharge(Button attackBtn, Player selectedPlayer, Attack selectedAttack)
        {
            AttackPanel.Visibility = Visibility.Collapsed;
            attackBtn.Content = "Select Attack";
            selectedPlayer.Charge(selectedAttack);

            ToggleTargets(selectedPlayer, selectedAttack, true);
            BattleField.SelectedAttack = null;
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            FloatingInventory inventory = new FloatingInventory(BattleField.SelectedPlayer, BattleField.TurnNum);
            inventory.ShowDialog();
            ForceItemsControlUpdate(AttackSelector);
        }

        private void Flee_Click(object sender, RoutedEventArgs e)
        {
            Player.Run(BattleField);
        }

        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            Player player = BattleField.SelectedPlayer;
            if (player.TurnsPassed < GameBase.NumTurns - 1)
            {
                BattleField.EndCharacterTurn(player);
            }
            else
            {
                BattleField.EndCharacterRound(player);
            }
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewStats(object sender, RoutedEventArgs e)
        {
            Character character = (Character)((Image)sender).Tag;

            if (character == null)
                return;

            List<Character> list = new List<Character>();
            list.Add(character);

            DetailedStatsContainer.Visibility = Visibility.Visible;
            DetailedStatsList.ItemsSource = list;
        }

        public void Exit()
        {
            Navigate("[back]");
        }

        public void NotifyAttackBegin(Attack attack, Character attacker, Character target, bool charging)
        {
            Dispatcher.Invoke(() =>
            {
                if (attacker.GetType() == typeof(Player))
                {
                    int index = Array.IndexOf(BattleField.ActivePlayers, (Player)attacker);
                    if (index != -1)
                    {
                        if (charging)
                            Animate("chargeAttack", playerImages[index]);
                        else
                        {
                            if (attacker.BattleFieldLocation.X < target.BattleFieldLocation.X)
                                Animate("simpleAttackRight", playerImages[index]);
                            else
                                Animate("simpleAttackLeft", playerImages[index]);
                        }
                    }
                }
                else
                {
                    int index = Array.IndexOf(BattleField.ActiveEnemies, (Enemy)attacker);
                    if (index != -1)
                    {
                        if (charging)
                            Animate("chargeAttack", enemyImages[index]);
                        else
                        {
                            if (attacker.BattleFieldLocation.X < target.BattleFieldLocation.X)
                                Animate("simpleAttackRight", enemyImages[index]);
                            else
                                Animate("simpleAttackLeft", enemyImages[index]);
                        }
                    }
                }
            });
        }

        public void NotifyAttackHit(Attack attack, Character target)
        {
            //Do animation
            Dispatcher.Invoke(() =>
            {
                ForceItemsControlUpdate(playerSnapshot);
                ForceItemsControlUpdate(enemySnapshot);
                ForceItemsControlUpdate(DetailedStatsList);
            });
        }

        public async void NotifyMovement(Character character, System.Drawing.Point oldLocation)
        {
            Dispatcher.Invoke(() =>
            {
                DoubleAnimation animationX = new DoubleAnimation((character.BattleFieldLocation.X - oldLocation.X) * BattleFieldGridColumnWidth, TimeSpan.FromSeconds(2));
                DoubleAnimation animationY = new DoubleAnimation((character.BattleFieldLocation.Y - oldLocation.Y) * BattleFieldGridRowHeight, TimeSpan.FromSeconds(2));

                Storyboard.SetTargetProperty(animationX, new PropertyPath(TranslateTransform.XProperty));
                Storyboard.SetTargetProperty(animationY, new PropertyPath(TranslateTransform.YProperty));

                Storyboard storyboard = new Storyboard();
                Image img;
                TransformGroup transformGroup = new TransformGroup();
                TranslateTransform translate = new TranslateTransform(0, 0);
                string translationName = $"translation{translate.GetHashCode()}";
                transformGroup.Children.Add(translate);

                if (character.GetType() == typeof(Player))
                {
                    int index = Array.IndexOf(BattleField.ActivePlayers, (Player)character);
                    img = playerImages[index];
                    //playerImages[index].BeginAnimation(TranslateTransform.XProperty, animationX);
                    //playerImages[index].BeginAnimation(TranslateTransform.YProperty, animationY);
                    //playerImages[index].RenderTransform = new TranslateTransform(0, 0);
                }
                else
                {
                    int index = Array.IndexOf(BattleField.ActiveEnemies, (Enemy)character);
                    img = enemyImages[index];
                    transformGroup.Children.Add(new ScaleTransform(-1, 1));
                    //enemyImages[index].BeginAnimation(TranslateTransform.XProperty, animationX);
                    //enemyImages[index].BeginAnimation(TranslateTransform.YProperty, animationY);
                    //enemyImages[index].RenderTransform = new TranslateTransform(0, 0);
                }

                img.RenderTransform = transformGroup;
                Storyboard.SetTargetName(img, translationName);
                storyboard.Children.Add(animationX);
                storyboard.Children.Add(animationY);

                //storyboard.Begin();
                Animate("characterMove", img);
            });

            await MiscMethods.TaskDelay(2000);

            Dispatcher.Invoke(() =>
            {
                if (character.GetType() == typeof(Player))
                {
                    int index = Array.IndexOf(BattleField.ActivePlayers, (Player)character);
                    UpdatePlayerLocation(index);
                    //playerImages[index].RenderTransform = new TranslateTransform(0, 0);
                }
                else
                {
                    int index = Array.IndexOf(BattleField.ActiveEnemies, (Enemy)character);
                    UpdateEnemyLocation(index);
                    //enemyImages[index].RenderTransform = new TranslateTransform(0, 0);
                }
            });
        }

        public void NotifyItemUsed(Item item, Character user)
        {
            //update sprites
            Dispatcher.Invoke(() =>
            {
                ForceItemsControlUpdate(playerSnapshot);
                ForceItemsControlUpdate(enemySnapshot);
                ForceItemsControlUpdate(DetailedStatsList);
            });
        }

        public void NotifyPlayerIsReady(Player player)
        {
            Dispatcher.Invoke(() =>
            {
                ActionPanel.Visibility = Visibility.Visible;
            });
        }

        public void NotifyDeath(Character character)
        {
            Dispatcher.Invoke(() =>
            {
                if (character.GetType() == typeof(Player))
                {
                    foreach (Image img in playerImages)
                    {
                        if (img.Tag != null && img.Tag.Equals(character))
                        {
                            Animate("deathLeft", img);
                            return;
                        }
                    }
                }
                else
                {
                    foreach (Image img in enemyImages)
                    {
                        if (img.Tag != null && img.Tag.Equals(character))
                        {
                            Animate("deathRight", img);
                            return;
                        }
                    }
                }
            });
        }

        public void NotifyPlayerLost()
        {
            BattleField = null;
            Navigate("[back]");
            MessageBox.Show("You just manage to escape.");
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

        private void InfoBlockScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
            {
                ScrollViewer scrollViewer = (ScrollViewer)sender;
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
