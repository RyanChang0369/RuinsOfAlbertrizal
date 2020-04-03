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
    /// Interaction logic for EditorMenu.xaml
    /// </summary>
    public partial class EditorMenu : Page
    {
        public EditorMenu()
        {
            InitializeComponent();
        }

        private void CreateNewPlayer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreatePlayerPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CreateNewEnemy(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateEnemyPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CreateNewMap(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void LoadPlayer(object sender, RoutedEventArgs e)
        {

        }
        private void LoadEnemy(object sender, RoutedEventArgs e)
        {

        }
        private void LoadMap(object sender, RoutedEventArgs e)
        {

        }
    }
}
