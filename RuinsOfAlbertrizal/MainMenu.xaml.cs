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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void NavEditorMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Editor/EditorMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ContinueCustomMap(object sender, RoutedEventArgs e)
        {
            GameLoader.LoadCustomMap();
            NavIntroInterface();
        }

        private void NewCampaign_Click(object sender, RoutedEventArgs e)
        {
            GameLoader.NewCampaign();
            NavIntroInterface();
        }

        private void ContinueCampaign_Click(object sender, RoutedEventArgs e)
        {
            GameLoader.LoadCampaign();
            NavIntroInterface();
        }

        private void NavIntroInterface()
        {
            this.NavigationService.Navigate(new Uri("IntroInterface.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
