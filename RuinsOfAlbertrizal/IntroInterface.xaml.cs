using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for IntroInterface.xaml
    /// </summary>
    public partial class IntroInterface : Page
    {
        public IntroInterface()
        {
            InitializeComponent();
            //ForwardNavigation();

            DataContext = GameBase.CurrentGame;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GameBase.CurrentGame.SeenIntroduction)
            {
                GameBase.CurrentGame.IntroMessage.InitializeControls(IntroText, NextBtn, SkipBtn);
                GameBase.CurrentGame.IntroMessage.Display();

                GameBase.CurrentGame.SeenIntroduction = true;
                FileHandler.SaveCurrentMap();
            }
            else
            {
                NavLevelIntro();
            }
        }

        /// <summary>
        /// All navigations point to the IntroInterface. Navigate players to the correct locations.
        /// </summary>
        private void ForwardNavigation()
        {
            if (GameBase.CurrentGame.SeenIntroduction)
            {
                NavLevelIntro();
            }
        }

        private void NavLevelIntro()
        {
            NavigationService.Navigate(new Uri("LevelIntroInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            NavLevelIntro();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentGame.IntroMessage.NextBtnIsSkip())
                NavLevelIntro();
        }

    }
}
