using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
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

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateMapPrompt.xaml
    /// </summary>
    public partial class CreateMapPrompt : Page
    {
        //public List<Player> CreatedPlayers = new List<Player>();
        public Player CreatedPlayer;
        public List<Enemy> CreatedEnemies = new List<Enemy>();
        public Boss CreatedBoss;

        //Just one level for now
        public Level CreatedLevel;

        public bool[] StepsDone = new bool[9];

        public CreateMapPrompt()
        {
            InitializeComponent();
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            for (int i = 0; i < StepsDone.Length; i++)
            {
                StepsDone[i] = false;
            }

            //Step 1: Player
            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                CreatedPlayer = CreatePlayerPrompt.CreatedPlayer;
                CreatePlayerBtn.Content = "Edit Player";
                CreatedPlayerLabel.Content = "Created Player: " + CreatedPlayer.SpecificName;
                StepsDone[0] = true;
            }

            //Step 2: Enemies
            if (CreateEnemyPrompt.CreatedEnemy != null)
            {
                CreatedEnemies.Add(CreateEnemyPrompt.CreatedEnemy);

                CreatedEnemiesTextBlock.Text = "";
                foreach (Enemy enemy in CreatedEnemies)
                {
                    CreatedEnemiesTextBlock.Text = CreatedEnemiesTextBlock.Text + enemy.SpecificName + "\r\n";
                }

                StepsDone[1] = true;
            }

            //Step 3: Bosses
            if (CreateBossPrompt.CreatedBoss != null)
            {
                CreatedBoss = CreateBossPrompt.CreatedBoss;

                CreatedBossesTextBlock.Text = CreatedBoss.SpecificName;

                StepsDone[2] = true;
            }


            //Checking for completeness
            foreach (bool stepDone in StepsDone)
            {
                if (!stepDone)
                {
                    SaveBtn.IsEnabled = false;
                    return;
                }
            }

            //SaveBtn.IsEnabled = true;
            //GameBase.NewGame();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {

        }
        private void Load(object sender, RoutedEventArgs e)
        {

        }

        private void NavPlayerPrompt(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreatePlayerPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void NavEnemyPrompt(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateEnemyPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void NavBossPrompt(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateBossPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
