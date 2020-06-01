using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace RuinsOfAlbertrizal
{
    public abstract class BaseWindow : Window
    {
        public void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Animate(string storyboardName, FrameworkElement element)
        {
            Storyboard storyboard = (Storyboard)Resources[storyboardName];
            storyboard.Begin(element);
        }
    }
}
