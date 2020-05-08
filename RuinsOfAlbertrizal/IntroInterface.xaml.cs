using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for IntroInterface.xaml
    /// </summary>
    public partial class IntroInterface : Page
    {
        public IntroInterface()
        {
            ForwardNavigation();
            InitializeComponent();

            DataContext = GameBase.CurrentGame;

            if (!GameBase.CurrentGame.SeenIntroduction || true)
            {
                GameBase.CurrentGame.IntroMessage.InitializeControls(IntroText, NextBtn, SkipBtn);
                GameBase.CurrentGame.IntroMessage.Display();

                GameBase.CurrentGame.SeenIntroduction = true;
            }
        }

        /// <summary>
        /// All navigations point to the IntroInterface. Navigate players to the correct locations.
        /// </summary>
        private void ForwardNavigation()
        {

        }

        private void NavAdventureInterface()
        {
            this.NavigationService.Navigate(new Uri("AdventureInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            NavAdventureInterface();
        }
    }
}
