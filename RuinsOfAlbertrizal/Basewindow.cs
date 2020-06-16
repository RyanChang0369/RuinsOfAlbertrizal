using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RuinsOfAlbertrizal
{
    public abstract class BaseWindow : Window
    {
        public void Close(object sender, RoutedEventArgs e)
        {
            Close();
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

        /// <summary>
        /// Forces the ItemsSource property of the listbox to update
        /// </summary>
        public static void ForceListBoxUpdate(ListBox listBox)
        {
            listBox.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            listBox.Items.Refresh();
        }
    }
}
