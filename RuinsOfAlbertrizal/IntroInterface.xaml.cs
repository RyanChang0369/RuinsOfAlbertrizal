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
            DataContext = GameBase.CurrentGame;
            Title = "Introduction: " + GameBase.CurrentGame.Name;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GameBase.CurrentGame.SeenIntroduction)
            {
                GameBase.CurrentGame.IntroMessage.InitializeControls(IntroText, NextBtn, SkipBtn);
                GameBase.CurrentGame.IntroMessage.Display();
            }
            else
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
            GameBase.CurrentGame.SeenIntroduction = true;
            FileHandler.SaveCurrentMap();
            NavLevelIntro();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentGame.IntroMessage.NextBtnIsNavigate())
                SkipBtn_Click(sender, e);
        }

    }
}
