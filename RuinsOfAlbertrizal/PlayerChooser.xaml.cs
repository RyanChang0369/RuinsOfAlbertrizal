using RuinsOfAlbertrizal.Characters;
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
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PlayerChooser.xaml
    /// </summary>
    public partial class PlayerChooser : Window
    {
        public Player SelectedPlayer { get; set; }
        public List<Player> ProvidedPlayers { get; set; }

        public PlayerChooser()
        {
            InitializeComponent();
        }

        public PlayerChooser(string title, List<Player> providedPlayers)
        {
            InitializeComponent();
            Title = title;
            ProvidedPlayers = providedPlayers;
            DataContext = this;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
