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
    public partial class LevelIntroInterface : Page
    {
        public LevelIntroInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            Title = "Introduction: " + GameBase.CurrentGame.CurrentLevel.Name;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GameBase.CurrentGame.CurrentLevel.IntroMessage.InitializeControls(IntroText, NextBtn, SkipBtn);
            GameBase.CurrentGame.CurrentLevel.IntroMessage.Display();
        }    

        private void NavAdventureInterface()
        {
            NavigationService.Navigate(new Uri("AdventureInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            GameBase.CurrentGame.CurrentLevel.SeenIntroduction = true;
            FileHandler.SaveCurrentMap();
            NavAdventureInterface();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentGame.CurrentLevel.IntroMessage.NextBtnIsNavigate())
                SkipBtn_Click(sender, e);
        }
    }
}
