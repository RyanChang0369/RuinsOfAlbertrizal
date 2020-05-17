﻿using RuinsOfAlbertrizal.Editor;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            Testing.InitTest();
        }

        private void NewCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.CreateCustomCampaign();
            }
            catch (Exception)
            {
                return;
            }

            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void EditCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.LoadCustomCampaign();
            }
            catch (ArgumentNullException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Project File Cannot Be Read!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ContinueCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.LoadCustomCampaign();
            }
            catch (ArgumentNullException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Project File Cannot Be Read!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            NavIntroInterface();
        }

        private void NewCampaign_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                GameLoader.NewCampaign();
            }
            catch (Exception)
            {
                MessageBox.Show("Program files not found.", "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            NavIntroInterface();
        }

        private void ContinueCampaign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GameLoader.LoadCampaign();
            }
            catch (Exception)
            {
                MessageBox.Show("Program files not found.", "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            NavIntroInterface();
        }

        private void NavIntroInterface()
        {
            NavigationService.Navigate(new Uri("IntroInterface.xaml", UriKind.RelativeOrAbsolute));
        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavIntroInterface();
        }

        private void ResetCustomMap(object sender, RoutedEventArgs e)
        {
            if (GameBase.CurrentMapLocation == null)
            {
                FileHandler.LoadCustomCampaign();
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the map? This action cannot be undone.", "Confirm Reset", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                GameBase.CurrentGame = GameBase.StaticGame;
                FileHandler.SaveStaticMap();
                FileHandler.SaveCurrentMap();

                MessageBox.Show("Map has been reset.");
            }
        }

        private void Reference(object sender, RoutedEventArgs e)
        {
            HelpSection helpSection = new HelpSection();
            helpSection.Show();
        }
    }
}
