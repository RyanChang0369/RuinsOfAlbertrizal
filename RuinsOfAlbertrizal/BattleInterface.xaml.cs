using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private List<Image> playerImages = new List<Image>();

        private List<Image> enemyImages = new List<Image>();

        private List<Enemy> Enemies { get; set; }

        private List<Enemy> AliveEnemies
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

        private List<Enemy> DeadEnemies
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

        private List<Enemy> ActiveEnemies
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

        public BattleInterface()
        {
            Enemies = new List<Enemy>();

            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            UpdateImageLists();
            UpdateGrid();
        }

        public BattleInterface(List<Enemy> enemies)
        {
            Enemies = enemies;

            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            UpdateImageLists();
            UpdateGrid();
        }

        private void UpdateImageLists()
        {
            ListBox optionsListBox_Player = new ListBox();
            ListBox optionsListBox_Enemy = new ListBox();

            playerImages = new List<Image>
            {
                player0, player1, player2, player3
            };

            enemyImages = new List<Image>
            {
                enemy0, enemy1, enemy2, enemy3
            };

            foreach (Image img in playerImages)
            {
                
            }

            foreach (Image img in enemyImages)
            {
            }
        }

        private void UpdateGrid()
        {
            for (int i = 0; i < GameBase.NumActiveCharacters; i++)
            {
                try
                {
                    playerImages[i].Source = GameBase.CurrentGame.ActivePlayers[i].WorldImgAsBitmapSource;
                    playerImages[i].Tag = GameBase.CurrentGame.ActivePlayers[i];
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    enemyImages[i].Source = ActiveEnemies[i].WorldImgAsBitmapSource;
                    enemyImages[i].Tag = ActiveEnemies[i];
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
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

        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Food_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
