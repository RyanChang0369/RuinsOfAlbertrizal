﻿using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal
{
    public abstract class BasePage : Page
    {
        /// <summary>
        /// Uses tags to navigate to correct editor page (relative uri).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Navigate(object sender, RoutedEventArgs e)
        {
            Control ctrl = (Control)sender;
            Navigate((string)ctrl.Tag);
        }

        public void Navigate(string location)
        {
            Navigate(new Uri(location, UriKind.RelativeOrAbsolute));
        }

        public void Navigate(Uri location)
        {
            NavigationService.Navigate(location);
        }

        public void SaveCurrentGame(object sender, RoutedEventArgs e)
        {
            FileHandler.SaveCurrentMap();
            MessageBox.Show("Game Saved!");
        }

        public void SaveStaticGame(object sender, RoutedEventArgs e)
        {
            FileHandler.SaveStaticMap();
            MessageBox.Show("Game Saved!");
        }
    }
}
