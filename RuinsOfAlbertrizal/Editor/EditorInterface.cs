using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public abstract partial class EditorInterface : Page
    {
        protected void Save(object sender, RoutedEventArgs e)
        {
            if (!FormIsValid())
            {
                MessageBox.Show("Please fill out all required forms", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        protected void Quit(object sender, RoutedEventArgs e)
        {
            ClearVariable();
            CreateMapPrompt.DoNotUpdate = true;
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        protected abstract void UpdateComponent();

        protected abstract bool FormIsValid();

        protected abstract void ClearVariable();
    }
}
