using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Editor;
using RuinsOfAlbertrizal.XMLInterpreter;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PlayerCreatePage.xaml
    /// </summary>
    public partial class PlayerCreatePage : BasePage
    {
        public static List<Player> CreatedPlayers { get; set; }

        public PlayerCreatePage()
        {
            InitializeComponent();

            DataContext = GameBase.CurrentGame;

            if (CreatedPlayers == null)
                CreatedPlayers = new List<Player>();

            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                CreatedPlayers.Add(CreatePlayerPrompt.CreatedPlayer);
            }
            else
            {
                CreatePlayerPrompt.CreatedPlayer = new Player();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (listOfChums.SelectedIndex < 0)
                return;

            CreatedPlayers.Remove((Player)listOfChums.SelectedItem);
            ForceListBoxUpdate(listOfChums);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            GameBase.CurrentGame.Players.AddRange(CreatedPlayers);
            GameBase.CurrentGame.PlayerCreated = true;

            foreach (Player player in GameBase.CurrentGame.Players)
            {
                player.InventoryEquiptments.AddRange(GameBase.CurrentGame.DefaultEquiptments);
                player.InventoryItems.AddRange(GameBase.CurrentGame.DefaultItems);
                player.InventoryConsumables.AddRange(GameBase.CurrentGame.DefaultConsumables);
            }

            FileHandler.SaveCurrentMap();

            Navigate("IntroInterface.xaml");
        }
    }
}
