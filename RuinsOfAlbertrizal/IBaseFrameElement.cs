using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal
{
    public interface IBaseFrameElement
    {
        void SaveCurrentGame(object sender, RoutedEventArgs e);

        void SaveStaticGame(object sender, RoutedEventArgs e);

        void Animate(string storyboardName, FrameworkElement element);
    }
}
