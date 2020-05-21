using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for AdventureInterface.xaml
    /// </summary>
    public partial class AdventureInterface : Page
    {
        public AdventureInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PartyMembersBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
