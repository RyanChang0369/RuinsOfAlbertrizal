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
        public static Map Map;

        public static int SelectedTab;

        public bool[] StepsDone = new bool[9];

        public CreateMapPrompt()
        {
            InitializeComponent();

            if (GameBase.CurrentGame == null)
                Map = new Map();
            else
                Map = GameBase.CurrentGame;

            DataContext = Map;

            UpdateComponent();
        }

        private void UpdateComponent()
        {
            MainTabControl.SelectedIndex = SelectedTab;

            //Step 1: Player
            if (Map.Player != null)
            {
                CreatePlayerBtn.Content = "Edit Player";
                StepsDone[0] = true;
            }

            //Step 2: Enemies
            if (CreateEnemyPrompt.CreatedEnemy != null)
            {
                Map.StoredEnemies.Add(CreateEnemyPrompt.CreatedEnemy);

                StepsDone[1] = true;
            }

            //Step 3: Bosses
            if (CreateBossPrompt.CreatedBoss != null)
            {
                Map.StoredBosses.Add(CreateBossPrompt.CreatedBoss);

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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SelectedTab = MainTabControl.SelectedIndex;
        }

        private void CreatedEnemiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Edit
        }

        private void CreatedBossesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Edit
        }
    }
}
