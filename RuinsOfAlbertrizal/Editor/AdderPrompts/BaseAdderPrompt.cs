using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    public abstract class BaseAdderPrompt : Window
    {
        protected bool saved = true;

        protected void ListChanged()
        {
            saved = false;
        }

        protected void Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected void Save(object sender, RoutedEventArgs e)
        {
            saved = true;
            Close();
        }

        protected void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (saved)
                return;

            MessageBoxResult result = MessageBox.Show("Save before quitting?", "Unsaved Work", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            { }
            else if (result == MessageBoxResult.Cancel)
                e.Cancel = true;
            else
                ResetVariable();
        }

        protected abstract void ResetVariable();
    }
}
