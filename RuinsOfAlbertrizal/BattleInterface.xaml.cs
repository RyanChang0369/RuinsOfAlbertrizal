using RuinsOfAlbertrizal.Characters;
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

        public BattleInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            
        }
    }
}
