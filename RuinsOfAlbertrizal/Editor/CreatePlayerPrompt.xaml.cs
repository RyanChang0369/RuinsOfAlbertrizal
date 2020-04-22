using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreatePlayerPrompt.xaml
    /// </summary>
    public partial class CreatePlayerPrompt : Page
    {
        public static Player CreatedPlayer { get; set; }
        public CreatePlayerPrompt()
        {
            InitializeComponent();
            CreatedPlayer = new Player();
            DataContext = CreatedPlayer;
            Load(null, null);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            Back(sender, null);
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CreatedPlayer == null)
                    return;

                if (CreatedPlayer.BaseStats[0] == 200)
                    Class.SelectedIndex = 0;
                else if (CreatedPlayer.BaseStats[0] == 150)
                    Class.SelectedIndex = 1;
                else if (CreatedPlayer.BaseStats[0] == 100)
                    Class.SelectedIndex = 2;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
