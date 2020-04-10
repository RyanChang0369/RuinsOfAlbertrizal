﻿using System;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void NavEditorMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ContinueCustomMap(object sender, RoutedEventArgs e)
        {
            try
            {
                GameLoader.LoadCustomMap();
            }
            catch (ArgumentNullException)
            {
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
            this.NavigationService.Navigate(new Uri("IntroInterface.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
