using RuinsOfAlbertrizal.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for ExploreInterface.xaml
    /// </summary>
    public partial class ExploreInterface : BasePage
    {
        public List<Player> ProvidedPlayers { get; set; }

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                selectedPlayer = value;

                if (selectedPlayer == null)
                    ExploreBtn.Content = "Select Player for Explore";
                else
                    ExploreBtn.Content = "Explore";
            }
        }

        public ExploreInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            Title = $"Exploring {GameBase.CurrentGame.CurrentLevel.Name}";
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer == null)
            {
                PlayerChooser chooser = new PlayerChooser("Select a player", ProvidedPlayers);
                chooser.ShowDialog();
                SelectedPlayer = chooser.SelectedPlayer;
            }
            else
            {
                
            }
        }
    }
}
