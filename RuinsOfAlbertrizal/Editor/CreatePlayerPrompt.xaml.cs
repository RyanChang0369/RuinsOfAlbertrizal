﻿using RuinsOfAlbertrizal.Characters;
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
            int[] numericalValues = new int[5];

            switch (Class.SelectedIndex)
            {
                case 0: //Warrior
                    numericalValues[0] = 200;
                    numericalValues[1] = 20;
                    numericalValues[2] = 10;
                    numericalValues[3] = 5;
                    numericalValues[4] = 5;
                    break;
                case 1: //Mage
                    numericalValues[0] = 150;
                    numericalValues[1] = 100;
                    numericalValues[2] = 2;
                    numericalValues[3] = 5;
                    numericalValues[4] = 7;
                    break;
                case 2: //Scout
                    numericalValues[0] = 100;
                    numericalValues[1] = 55;
                    numericalValues[2] = 2;
                    numericalValues[3] = 7;
                    numericalValues[4] = 10;
                    break;
            }

            CreatedPlayer.BaseStats = numericalValues;
            //FileHandler.SavePlayer(CreatedPlayer);
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
