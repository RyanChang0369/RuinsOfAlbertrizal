using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
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
                PlayerChooser chooser = new PlayerChooser("Select a player", GameBase.CurrentGame.AlivePlayers);
                chooser.ShowDialog();
                SelectedPlayer = chooser.SelectedPlayer;
            }
            else
            {
                DoRandomEvent();
            }
        }

        private void DoRandomEvent()
        {
            List<RandomEvent> randomEvents = new List<RandomEvent>
            {
                new RandomEvent("Find Item", 0.0),          //Original value: 0.2
                new RandomEvent("Enemy Encounter", 0.2),    //Original value: 0.2
                new RandomEvent("Find Team Member", 0.0),   //Original value: 0.1
                new RandomEvent("Nothing", 0.0)             //Original value: 0.5
            };

            RandomEventChooser randomEventChooser = new RandomEventChooser(randomEvents);

            RandomEvent choosenEvent = randomEventChooser.GetSelectedRandomEvent();

            switch(choosenEvent.Tag)
            {
                case "Find Item":
                    DoFindItem();
                    break;
                case "Enemy Encounter":
                    DoRandomEncounter();
                    break;
                case "Find Team Member":
                    DoFindTeamMember();
                    break;
                case "Nothing":
                    MessageBox.Show("After wandering the surrounding area for a few minutes, you find nothing.");
                    break;
            }
        }

        private void DoFindItem()
        {
            //Check if these items affect the stored items.
            List<RandomEvent> randomEventsItems = new List<RandomEvent>();
            foreach (Item item in GameBase.CurrentGame.StoredItems)
            {
                randomEventsItems.Add(new RandomEvent(item, item.DropChance));
            }
            Item selectedItem = (Item)new RandomEventChooser(randomEventsItems).GetSelectedTag();

            List<RandomEvent> randomEventsEquiptments = new List<RandomEvent>();
            foreach (Equiptment equiptment in GameBase.CurrentGame.StoredEquiptments)
            {
                randomEventsEquiptments.Add(new RandomEvent(equiptment, equiptment.DropChance));
            }
            Equiptment selectedEquiptment = (Equiptment)new RandomEventChooser(randomEventsEquiptments).GetSelectedTag();

            List<RandomEvent> randomEventsConsumables = new List<RandomEvent>();
            foreach (Consumable consumable in GameBase.CurrentGame.StoredConsumables)
            {
                randomEventsConsumables.Add(new RandomEvent(consumable, consumable.DropChance));
            }
            Consumable selectedConsumable = (Consumable)new RandomEventChooser(randomEventsConsumables).GetSelectedTag();

            switch (RNG.GetRandomInteger(3))
            {
                case 0:
                    SelectedPlayer.ObtainItem(selectedItem);
                    break;
                case 1:
                    SelectedPlayer.ObtainEquiptment(selectedEquiptment);
                    break;
                case 2:
                    SelectedPlayer.ObtainConsumable(selectedConsumable);
                    break;
            }
        }

        private void DoRandomEncounter()
        {
            GameBase.CurrentGame.CurrentBattleField = new BattleField();
            Navigate(GameBase.CurrentGame.CurrentBattleField.BattleInterface);
        }

        private void DoFindTeamMember()
        {

        }
    }
}
