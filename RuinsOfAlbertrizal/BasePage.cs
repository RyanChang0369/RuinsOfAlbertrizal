using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

            string path = (string)ctrl.Tag;

            switch (path)
            {
                case "[back]":
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                    break;
                case "[forward]":
                    if (NavigationService.CanGoForward)
                        NavigationService.GoForward();
                    break;
                default:
                    Navigate(path);
                    break;
            }
        }

        public void Navigate(string location)
        {
            Navigate(new Uri(location, UriKind.RelativeOrAbsolute));
        }

        public void Navigate(Uri location)
        {
            NavigationService.Navigate(location);
        }

        public void Navigate(Page page)
        {
            NavigationService.Navigate(page);
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

        public void Animate(string storyboardName, FrameworkElement element)
        {
            Storyboard storyboard = (Storyboard)Resources[storyboardName];
            storyboard.Begin(element);
        }
    }
}
