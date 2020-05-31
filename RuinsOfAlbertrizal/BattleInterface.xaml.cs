using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
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
        private BattleField battleField = GameBase.CurrentGame.BattleField;

        private List<Image> playerImages = new List<Image>();

        private List<Image> enemyImages = new List<Image>();

        private List<Image> playerTargetImages = new List<Image>();

        private List<Image> enemyTargetImages = new List<Image>();

        public BattleInterface()
        {
            battleField = new BattleField();

            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
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
                try
                {
                    if (GameBase.CurrentGame.ActivePlayers[i].WorldImgIsValid)
                        playerImages[i].Source = GameBase.CurrentGame.ActivePlayers[i].WorldImgAsBitmapSource;
                    else
                        playerImages[i].Source = new BitmapImage();

                    playerImages[i].Tag = GameBase.CurrentGame.ActivePlayers[i];
                    playerTargetImages[i].Tag = GameBase.CurrentGame.ActivePlayers[i];
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (battleField.Enemies[i].WorldImgIsValid)
                        enemyImages[i].Source = battleField.Enemies[i].WorldImgAsBitmapSource;
                    else
                        enemyImages[i].Source = new BitmapImage();

                    enemyImages[i].Tag = battleField.ActiveEnemies[i];
                    enemyTargetImages[i].Tag = battleField.ActiveEnemies[i];
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
        }

        private void ShowTargets()
        {
            foreach (Image image in playerTargetImages)
            {
                Animate("targetFadeIn", image);
            }

            foreach (Image image in enemyTargetImages)
            {
                Animate("targetFadeIn", image);
            }
        }

        private void HideTargets()
        {
            foreach (Image image in playerTargetImages)
            {
                Animate("targetFadeOut", image);
            }

            foreach (Image image in enemyTargetImages)
            {
                Animate("targetFadeOut", image);
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
            throw new NotImplementedException();
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Flee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
