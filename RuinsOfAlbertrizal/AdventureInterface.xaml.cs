using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for AdventureInterface.xaml
    /// </summary>
    public partial class AdventureInterface : BasePage
    {
        public AdventureInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            FileHandler.SaveCurrentMap();

            MessageBox.Show("Game Saved!");
        }
    }
}
