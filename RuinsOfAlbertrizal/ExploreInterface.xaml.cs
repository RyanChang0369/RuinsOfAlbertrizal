﻿using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.XMLInterpreter;
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
        public ExploreInterface()
        {
            InitializeComponent();
            DataContext = GameBase.CurrentGame.CurrentLevel;
            Title = $"Exploring {GameBase.CurrentGame.CurrentLevel.Name}";
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
                    MessageBox.Show("After wandering the surrounding area for a few minutes, you find nothing.");
                    break;
            }
        }

        private void DoFindItem()
        {
            //Check if stored items are affected
            List<RandomEvent> randomEvents = new List<RandomEvent>();
            foreach (Item item in GameBase.CurrentGame.AllItems)
            {
                randomEvents.Add(new RandomEvent(item, item.DropChance));
            }
            Item selected = (Item)new RandomEventChooser(randomEvents).GetSelected();

            GameBase.CurrentGame.PlayerObtainObject(selected);

            ////Check if these items affect the stored items.
            //List<RandomEvent> randomEventsItems = new List<RandomEvent>();
            //foreach (Item item in GameBase.CurrentGame.StoredItems)
            //{
            //    randomEventsItems.Add(new RandomEvent(item, item.DropChance));
            //}
            //Item selectedItem = (Item)new RandomEventChooser(randomEventsItems).GetSelected();

            //List<RandomEvent> randomEventsEquiptments = new List<RandomEvent>();
            //foreach (Equiptment equiptment in GameBase.CurrentGame.StoredEquiptments)
            //{
            //    randomEventsEquiptments.Add(new RandomEvent(equiptment, equiptment.DropChance));
            //}
            //Equiptment selectedEquiptment = (Equiptment)new RandomEventChooser(randomEventsEquiptments).GetSelected();

            //List<RandomEvent> randomEventsConsumables = new List<RandomEvent>();
            //foreach (Consumable consumable in GameBase.CurrentGame.StoredConsumables)
            //{
            //    randomEventsConsumables.Add(new RandomEvent(consumable, consumable.DropChance));
            //}
            //Consumable selectedConsumable = (Consumable)new RandomEventChooser(randomEventsConsumables).GetSelected();

            //switch (RNG.GetRandomInteger(3))
            //{
            //    case 0:
            //        GameBase.CurrentGame.ObtainItem(selectedItem);
            //        break;
            //    case 1:
            //        GameBase.CurrentGame.ObtainEquiptment(selectedEquiptment);
            //        break;
            //    case 2:
            //        GameBase.CurrentGame.ObtainConsumable(selectedConsumable);
            //        break;
            //}

            FileHandler.SaveCurrentMap();
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
