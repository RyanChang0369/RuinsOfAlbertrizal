﻿using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Inventory;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Drawing.Point;

namespace RuinsOfAlbertrizal.Environment
{
    /// <summary>
    /// Interaction logic for BattleInterface.xaml
    /// </summary>
    public partial class BattleInterface : BasePage
    {
        public int BattleFieldGridRowHeight => (int)BattleFieldGrid.ActualHeight / BattleField.BattleFieldHeight;

        public int BattleFieldGridColumnWidth => 200;

        public bool AnimatingMovement { get; set; }

        private bool PlayerMoving { get; set; }

        private Point OriginalPlayerPosition { get; set; }

        public BattleField BattleField { get; set; }

        private List<CharacterImage> playerImages = new List<CharacterImage>();

        private List<CharacterImage> enemyImages = new List<CharacterImage>();

        private Button[,] movementIndicators = new Button[BattleField.BattleFieldHeight, BattleField.BattleFieldWidth];

        public BattleInterface(BattleField battleField)
        {
            BattleField = battleField;
            InitialPositionCharacters();
            InitializeComponent();
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

            for (int i = 0; i < BattleField.BattleFieldHeight; i++)
            {
                for (int j = 0; j < BattleField.BattleFieldWidth; j++)
                {
                    Button btn = new Button
                    {
                        Background = new SolidColorBrush(Colors.LightCyan),
                        BorderThickness = new Thickness(0),
                        Opacity = 0.5,
                        Visibility = Visibility.Collapsed,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(0),
                    };

                    btn.Click += MoveTileSelect;

                    BattleFieldGrid.Children.Add(btn);
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    movementIndicators[i, j] = btn;
                }
            }
        }

        private void UpdateImageLists()
        {
            playerImages = new List<CharacterImage>
            {
                player0, player1, player2, player3
            };

            enemyImages = new List<CharacterImage>
            {
                enemy0, enemy1, enemy2, enemy3
            };
        }

        private void InitialPositionCharacters()
        {
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                if (BattleField.ActivePlayers[i] != null)
                {
                    BattleField.ActivePlayers[i].BattleFieldLocation = new Point(7, i);
                }

                if (BattleField.ActiveEnemies[i] != null)
                {
                    BattleField.ActiveEnemies[i].BattleFieldLocation = new Point(12, i);
                }
            }
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

        private async void SlideOutOverlay()
        {
            
        }

        //private void UpdateCharacterLocation(int i, Type characterType)
        //{
        //    if (characterType == typeof(Player))
        //    {
        //        UpdatePlayerLocation(i);
        //    }
        //    else
        //    {
        //        UpdateEnemyLocation(i);
        //    }
        //}

        //private void UpdatePlayerLocation(int i)
        //{
        //    try
        //    {
        //        playerImages[i].CharacterMove();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //private void UpdateEnemyLocation(int i)
        //{
        //    try
        //    {
        //        enemyImages[i].CharacterMove();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public void SwapEnemy(int index)
        {
            Animate("enemySlideOut", enemyImages[index]);
            UpdateEnemyImage(index);
            Animate("enemySlideIn", enemyImages[index]);
        }

        public void SwapPlayer(int index)
        {
            Animate("playerSlideOut", playerImages[index]);
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
                    playerImages[i].Select();
                }
            }

            foreach (int i in attackableEnemies)
            {
                if (BattleField.ActiveEnemies[i] != null)
                {
                    enemyImages[i].Select();
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
                if (playerImages[i].AssociatedCharacter != null)
                {
                    playerImages[i].Deselect();
                }
            }

            foreach (int i in attackableEnemies)
            {
                if (enemyImages[i].AssociatedCharacter != null)
                {
                    enemyImages[i].Deselect();
                }
            }
        }

        private void CollapseAllBtnPanels()
        {
            ActionBtnsPanel.Visibility = Visibility.Collapsed;
            AttackBtnsPanel.Visibility = Visibility.Collapsed;
            MoveBtnsPanel.Visibility = Visibility.Collapsed;
        }

        private async void InitialAnimation()
        {
            await MiscMethods.TaskDelay(1500);

            Animate("overlaySlideRight", OverlayPanel1);
            Animate("overlaySlideLeft", OverlayPanel2);

            await MiscMethods.TaskDelay(1990);

            Overlay.Visibility = Visibility.Collapsed;

            foreach (CharacterImage image in playerImages)
            {
                await MiscMethods.TaskDelay(50);
                image.InitialAnimation();
            }

            foreach (CharacterImage image in enemyImages)
            {
                await MiscMethods.TaskDelay(50);
                image.InitialAnimation();
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            AttackBtnsPanel.Visibility = Visibility.Visible;
            MoveBtnsPanel.Visibility = Visibility.Collapsed;
            ActionBtnsPanel.Visibility = Visibility.Collapsed;

            SelectAttack();

            //if (BattleField.SelectedAttack == null)
            //    SelectAttack(btn);
            //else if (!BattleField.SelectedAttack.IsCharged())
            //    DoCharge(btn, BattleField.SelectedPlayer, BattleField.SelectedAttack);
            //else if (BattleField.SelectedTarget != null)
            //    DoAttack(btn, BattleField.SelectedPlayer, BattleField.SelectedTarget, BattleField.SelectedAttack);
        }

        private void ConfirmAttackBtn_Click(object sender, RoutedEventArgs e)
        {
            CancelAttackBtn_Click(sender, e);

            if (!BattleField.SelectedAttack.IsCharged())
                DoCharge(BattleField.SelectedPlayer, BattleField.SelectedAttack);
            else if (BattleField.SelectedTarget != null)
                DoAttack(BattleField.SelectedPlayer, BattleField.SelectedTarget, BattleField.SelectedAttack);
        }

        private void CancelAttackBtn_Click(object sender, RoutedEventArgs e)
        {
            AttackBtnsPanel.Visibility = Visibility.Collapsed;
            ActionBtnsPanel.Visibility = Visibility.Visible;
            AttackBtnsPanel.Visibility = Visibility.Collapsed;
            AttackPanel.Visibility = Visibility.Collapsed;
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
                ConfirmAttackBtn.Content = "Confirm Attack";
                ConfirmAttackBtn.IsEnabled = true;
            }
            else if (!BattleField.SelectedAttack.IsCooledDown())
            {
                MessageBox.Show("Attack is cooling down");
            }
            else
            {
                ConfirmAttackBtn.Content = "Charge Attack";
                ConfirmAttackBtn.IsEnabled = true;
            }
        }

        private void SelectAttack()
        {
            AttackPanel.Visibility = Visibility.Visible;
            ForceItemsControlUpdate(AttackSelector);
        }

        //private void TargetImage_MouseUp(object sender, RoutedEventArgs e)
        //{
        //    CharacterImage img = (CharacterImage)sender;

        //    if (img.Opacity != 0.9)
        //    {
        //        try
        //        {
        //            BattleField.SelectedTarget = (Character)img.Tag;
        //        }
        //        catch (InvalidCastException)
        //        {
        //            //BattleField.SelectedSide = (List<Character>)img.Tag;
        //            throw;
        //        }

        //        Animate("targetConfirm", img);
        //        AttackBtn.IsEnabled = true;
        //    }
        //    else
        //    {
        //        BattleField.SelectedTarget = null;
        //        Animate("targetDeconfirm", img);
        //        AttackBtn.IsEnabled = false;
        //    }

        //    foreach (CharacterImage image in playerImages)
        //    {
        //        if (image.Opacity == 0.9 && image != img)
        //        {
        //            Animate("targetDeconfirm", image);
        //        }
        //    }

        //    foreach (CharacterImage image in enemyImages)
        //    {
        //        if (image.Opacity == 0.9 && image != img)
        //        {
        //            Animate("targetDeconfirm", image);
        //        }
        //    }
        //}

        private void CharacterImage_RequestDetailedStats(object sender, RoutedEventArgs e)
        {
            CharacterImage img = (CharacterImage)sender;

            Character character = img.AssociatedCharacter;

            if (character == null)
                return;

            List<Character> list = new List<Character>
            {
                character
            };

            DetailedStatsContainer.Visibility = Visibility.Visible;
            DetailedStatsList.ItemsSource = list;
        }

        private void CharacterImage_TargetConfirm(object sender, RoutedEventArgs e)
        {
            CharacterImage charimg = (CharacterImage)sender;

            BattleField.SelectedTarget = charimg.AssociatedCharacter;
        }

        private void CharacterImage_TargetDeconfirm(object sender, RoutedEventArgs e)
        {
            CharacterImage charimg = (CharacterImage)sender;

            if (BattleField.SelectedTarget == charimg.AssociatedCharacter)
            {
                BattleField.SelectedTarget = null;
            }
        }

        private void DoAttack(Player selectedPlayer, Character target, Attack selectedAttack)
        {
            //Hide panel
            AttackPanel.Visibility = Visibility.Collapsed;

            selectedPlayer.DoAttack(selectedAttack, target);

            ToggleTargets(selectedPlayer, selectedAttack, true);
            BattleField.SelectedAttack = null;
        }

        private void DoCharge(Player selectedPlayer, Attack selectedAttack)
        {
            AttackPanel.Visibility = Visibility.Collapsed;
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
            PlayerMoving = true;
            Player player = BattleField.SelectedPlayer;

            AttackBtnsPanel.Visibility = Visibility.Collapsed;
            MoveBtnsPanel.Visibility = Visibility.Visible;
            ActionBtnsPanel.Visibility = Visibility.Collapsed;

            Point location = player.BattleFieldLocation;
            int moveRange = player.CurrentStats[4];
                
            int rightEdge = Math.Max(location.X - moveRange + 1, 0);
            int leftEdge = Math.Min(location.X + moveRange, BattleField.BattleFieldWidth);
            int topEdge = Math.Max(location.Y - moveRange, 0);
            int bottomEdge = Math.Min(location.Y + 2 * moveRange + 1, BattleField.BattleFieldHeight);

            for (int i = topEdge; i < bottomEdge; i++)
            {
                int jaggedRightEdge = Math.Max(rightEdge + Math.Abs(i - location.Y), 0);
                int jaggedLeftEdge = Math.Min(leftEdge - Math.Abs(i - location.Y) + 1, BattleField.BattleFieldWidth);
                for (int j = jaggedRightEdge; j <= jaggedLeftEdge; j++)
                {
                    if (j >= BattleField.BattleFieldWidth)
                        continue;

                    movementIndicators[i, j].Visibility = Visibility.Visible;
                }
            }

            OriginalPlayerPosition = player.BattleFieldLocation;
        }

        private void ConfirmMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            BattleField.FinalizeMovement(BattleField.SelectedPlayer, BattleField.SelectedPlayer.BattleFieldLocation);

            CancelMoveBtn_Click(sender, e);
        }

        private void CancelMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            MoveBtnsPanel.Visibility = Visibility.Collapsed;
            ActionBtnsPanel.Visibility = Visibility.Visible;
            PlayerMoving = false;
            ConfirmMoveBtn.IsEnabled = false;

            for (int i = 0; i < BattleField.BattleFieldHeight; i++)
            {
                for (int j = 0; j < BattleField.BattleFieldWidth; j++)
                {
                    movementIndicators[i, j].Visibility = Visibility.Collapsed;
                }
            }

            Player player = BattleField.SelectedPlayer;
            Point tempPoint = player.BattleFieldLocation;

            player.BattleFieldLocation = OriginalPlayerPosition;

            BattleField.FakeMovement(player, tempPoint);
        }

        private void MoveTileSelect(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Player player = BattleField.SelectedPlayer;

            Point lastPlayerPosition = player.BattleFieldLocation;

            int x = Grid.GetColumn(btn);
            int y = Grid.GetRow(btn);

            player.BattleFieldLocation = new Point(x, y);
            BattleField.FakeMovement(player, lastPlayerPosition);

            ConfirmMoveBtn.IsEnabled = true;
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
                    if (charging && index != -1)
                    {
                        playerImages[index].CharcterCharge();
                    }
                    else if (index != -1)
                    {
                        playerImages[index].CharacterAttack(attacker.BattleFieldLocation, target.BattleFieldLocation);
                    }
                }
                else
                {
                    int index = Array.IndexOf(BattleField.ActiveEnemies, (Enemy)attacker);
                    if (charging && index != -1)
                    {
                        enemyImages[index].CharcterCharge();
                    }
                    else if (index != -1)
                    {
                        enemyImages[index].CharacterAttack(attacker.BattleFieldLocation, target.BattleFieldLocation);
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

        public async Task NotifyMovement(Character character, Point oldLocation)
        {
            await Dispatcher.Invoke(async () =>
            {
                AnimatingMovement = true;
                int index = -1;
                if (character.GetType() == typeof(Player))
                {
                    index = Array.IndexOf(BattleField.ActivePlayers, character);
                    await playerImages[index].CharacterMove(oldLocation, BattleFieldGridColumnWidth, BattleFieldGridRowHeight);
                }
                else
                {
                    index = Array.IndexOf(BattleField.ActiveEnemies, character);
                    await enemyImages[index].CharacterMove(oldLocation, BattleFieldGridColumnWidth, BattleFieldGridRowHeight);
                }
                AnimatingMovement = false;
            });

            return;
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

        public void NotifyPlayerRoundStart()
        {
            Dispatcher.Invoke(() =>
            {
                ActionBtnsPanel.Visibility = Visibility.Visible;
            });
        }

        public void NotifyEnemyRoundStart()
        {
            Dispatcher.Invoke(() =>
            {
                CollapseAllBtnPanels();
            });
        }

        public void NotifyDeath(Character character)
        {
            Dispatcher.Invoke(() =>
            {
                if (character.GetType() == typeof(Player))
                {
                    foreach (CharacterImage img in playerImages)
                    {
                        if (img.AssociatedCharacter != null && img.AssociatedCharacter.Equals(character))
                        {
                            img.CharacterDeath();
                            return;
                        }
                    }
                }
                else
                {
                    foreach (CharacterImage img in enemyImages)
                    {
                        if (img.AssociatedCharacter != null && img.AssociatedCharacter.Equals(character))
                        {
                            img.CharacterDeath();
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
