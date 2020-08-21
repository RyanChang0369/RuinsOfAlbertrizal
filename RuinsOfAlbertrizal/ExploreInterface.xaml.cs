using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.Text;
using RuinsOfAlbertrizal.XMLInterpreter;
using System.Collections.Generic;
using System.Windows;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for ExploreInterface.xaml
    /// </summary>
    public partial class ExploreInterface : BasePage
    {
        public ExploreInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            Title = $"Exploring {GameBase.CurrentGame.CurrentLevel.Name}";
            Testing.ExploreInterfaceTest();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to save before exiting?", "Save?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    FileHandler.SaveCurrentMap();
                    Navigate("MainMenu.xaml");
                    break;
                case MessageBoxResult.No:
                    Navigate("MainMenu.xaml");
                    break;
                default:
                    return;
            }
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentGame.ActivePlayers.IsAllNull())
            {
                MessageBox.Show("Please select at least one player to be in your party.", "Insufficient party members", MessageBoxButton.OK, MessageBoxImage.Error);
                ExploreBtn.IsEnabled = false;
            }
            else
                DoRandomEvent();
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
                    DoFindNothing();
                    break;
            }
        }

        private void DoFindItem()
        {
            List<RandomEvent> randomEvents = new List<RandomEvent>();
            foreach (Item item in GameBase.CurrentGame.AllItems)
            {
                randomEvents.Add(new RandomEvent(item, item.DropChance));
            }
            Item selected = (Item)new RandomEventChooser(randomEvents).GetSelected();

            GameBase.CurrentGame.PlayerObtainObject(selected);

            FileHandler.SaveCurrentMap();
        }

        private async void DoRandomEncounter()
        {
            overlay.Visibility = Visibility.Visible;
            Animate("overlayAppear", overlay);

            await MiscMethods.TaskDelay(1000);

            GameBase.CurrentGame.CurrentBattleField = new BattleField();

            Navigate(GameBase.CurrentGame.CurrentBattleField.BattleInterface);
        }

        private void DoFindTeamMember()
        {
            int aveBI = GameBase.CurrentGame.Players.AverageBI(false);

            List<Enemy> possibleTeamMembers = GameBase.StaticGame.StoredEnemies.FindAll(enemy => enemy.BattleIndex <= aveBI);
            int fateSelector = RNG.GetRandomInteger(possibleTeamMembers.Count);
            Enemy teamMember = possibleTeamMembers[fateSelector];

            while (teamMember.BattleIndex < aveBI)
            {
                teamMember.Level++;
            }

            if (teamMember.Level > 1)
                teamMember.Level--;

            GameBase.CurrentGame.FindTeamMember(teamMember.RoAMemoryClone());
            FileHandler.SaveCurrentMap();
        }

        private void DoFindNothing()
        {
            MessageBox.Show("After wandering the surrounding area for a few minutes, you find nothing.");
        }
    }
}
