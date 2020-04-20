using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;
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
        public Map Map;
        //public List<Player> CreatedPlayers = new List<Player>();

        //Just one level for now

        public bool[] StepsDone = new bool[9];

        public CreateMapPrompt()
        {
            InitializeComponent();
            
            Map = GameBase.CurrentGame;

            if (Map == null)
                Map = new Map();

            DataContext = Map;

            UpdateComponent();
        }

        private void UpdateComponent()
        {
            //Step 1: Player
            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                Map.Player = CreatePlayerPrompt.CreatedPlayer;
                CreatePlayerBtn.Content = "Edit Player";
                StepsDone[0] = true;
            }
            else if (Map.Player != null)
            {
                CreatePlayerPrompt.CreatedPlayer = Map.Player;
                CreatePlayerBtn.Content = "Edit Player";
                StepsDone[0] = true;
            }

            //Step 2: Enemies
            if (CreateEnemyPrompt.CreatedEnemy != null)
            {
                Map.StoredEnemies.Add(CreateEnemyPrompt.CreatedEnemy);

                StepsDone[1] = true;
            }
            else if (Map.StoredEnemies != null)
            {
                CreateEnemyPrompt.CreatedEnemy = Map.StoredEnemies[Map.StoredEnemies.Count - 1];

                StepsDone[1] = true;
            }

            //Step 3: Bosses
            if (CreateBossPrompt.CreatedBoss != null)
            {
                Map.StoredBosses[0] = CreateBossPrompt.CreatedBoss;

                StepsDone[2] = true;
            }
            else if (Map.StoredBosses.Count > 0 && Map.StoredBosses[0] != null)
            {
                CreateBossPrompt.CreatedBoss = Map.StoredBosses[0];

                StepsDone[2] = true;
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
            GameBase.CurrentGame = Map;
            FileHandler.SaveObject(typeof(Map), Map, GameBase.CustomMapLocation);
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            FileHandler.LoadCustomCampaign();
            Map = GameBase.CurrentGame;
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
