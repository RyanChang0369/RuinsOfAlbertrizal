using RuinsOfAlbertrizal.XMLInterpreter;
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
    public partial class LevelIntroInterface : Page
    {
        public LevelIntroInterface()
        {
            InitializeComponent();

            DataContext = GameBase.CurrentGame.CurrentLevel;

            if (!GameBase.CurrentGame.CurrentLevel.SeenIntroduction)
            {
                GameBase.CurrentGame.CurrentLevel.IntroMessage.InitializeControls(IntroText, NextBtn, SkipBtn);
                GameBase.CurrentGame.CurrentLevel.IntroMessage.Display();

                GameBase.CurrentGame.CurrentLevel.SeenIntroduction = true;
                FileHandler.SaveCurrentMap();
            }
            else
                NavAdventureInterface();
        }

        private void NavAdventureInterface()
        {
            NavigationService.Navigate(new Uri("AdventureInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            NavAdventureInterface();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentGame.CurrentLevel.IntroMessage.NextBtnIsSkip())
                NavAdventureInterface();
        }
    }
}
