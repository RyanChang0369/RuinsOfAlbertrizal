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
            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                CreatedPlayer = CreatePlayerPrompt.CreatedPlayer;
                CreatePlayerBtn.IsEnabled = false;
                StepsDone[0] = true;
            }
            if (CreatedEnemies != null)
                StepsDone[1] = true;


            foreach (bool stepDone in StepsDone)
            {
                if (!stepDone)
                    return;
            }

            GameBase.NewGame();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/EditorMenu.xaml", UriKind.RelativeOrAbsolute));
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
    }
}
