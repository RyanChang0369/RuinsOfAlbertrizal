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
            StatusBar.Content = "";

            //Step 1: Player
            if (Map.Player != null)
            {
                CreatePlayerBtn.Content = "Edit Player";
                StepsDone[0] = true;
            }

            //Step 2: Enemies
            if (CreateEnemyPrompt.CreatedEnemy != null)
            {
                if (!Map.StoredEnemies.Contains(CreateEnemyPrompt.CreatedEnemy))
                    Map.StoredEnemies.Add(CreateEnemyPrompt.CreatedEnemy);

                StepsDone[1] = true;
            }

            //Step 3: Bosses
            if (CreateBossPrompt.CreatedBoss != null)
            {
                if (!Map.StoredBosses.Contains(CreateBossPrompt.CreatedBoss))
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
            StatusBar.Content = "Saving...";
            GameBase.CurrentGame = Map;
            FileHandler.SaveObject(typeof(Map), Map, GameBase.CustomMapLocation);
            
            StatusBar.Content = "Saved!";
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
            try
            {
                CreateEnemyPrompt.CreatedEnemy = Map.StoredEnemies[CreatedEnemiesList.SelectedIndex];
                CreateEnemyBtn.Content = "Edit Enemy";
            }
            catch (Exception)
            {

            }
        }

        private void CreatedBossesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CreateBossPrompt.CreatedBoss = Map.StoredBosses[CreatedBossesList.SelectedIndex];
                CreateBossBtn.Content = "Edit Boss";
            }
            catch (Exception)
            {

            }
        }

        private void ClearSelectionEnemy(object sender, RoutedEventArgs e)
        {
            CreatedEnemiesList.SelectedIndex = -1;
            CreateEnemyBtn.Content = "Create Enemy";
        }

        private void ClearSelectionBoss(object sender, RoutedEventArgs e)
        {
            CreatedBossesList.SelectedIndex = -1;
            CreateBossBtn.Content = "Create Boss";
        }

        private void DeleteSelectionEnemy(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateEnemyPrompt.CreatedEnemy = new Enemy();
                Map.StoredEnemies.RemoveAt(CreatedEnemiesList.SelectedIndex);
                CreatedEnemiesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                ClearSelectionEnemy(sender, null);
            }
            catch (Exception)
            {
                
            }
        }

        private void DeleteSelectionBoss(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateBossPrompt.CreatedBoss = new Boss();
                Map.StoredBosses.RemoveAt(CreatedBossesList.SelectedIndex);
                CreatedBossesList.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
                ClearSelectionBoss(sender, null);
            }
            catch (Exception)
            {

            }
        }
    }
}
