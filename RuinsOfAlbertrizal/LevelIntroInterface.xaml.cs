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
