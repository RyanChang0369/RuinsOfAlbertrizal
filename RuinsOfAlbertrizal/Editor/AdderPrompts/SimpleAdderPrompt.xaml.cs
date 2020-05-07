using RuinsOfAlbertrizal.Mechanics;
using System;
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
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    /// <summary>
    /// Interaction logic for SimpleAdderPrompt.xaml
    /// </summary>
    public partial class SimpleAdderPrompt : Window
    {
        private bool saved = false;

        public List<ObjectOfAlbertrizal> TargetObjects { get; set; }

        public List<ObjectOfAlbertrizal> StoredObjects { get; set; }

        public SimpleAdderPrompt()
        {
            InitializeComponent();
        }

        public SimpleAdderPrompt(List<ObjectOfAlbertrizal> targetObjects, List<ObjectOfAlbertrizal> storedObjects, string title)
        {
            InitializeComponent();

            if (targetObjects == null)
                TargetObjects = new List<ObjectOfAlbertrizal>();
            else
            {
                TargetObjects = targetObjects;

                for (int i = 0; i < TargetObjects.Count; i++)
                {
                    AddedObjectsList.Items.Add(TargetObjects[i]);
                }
            }

            if (storedObjects == null)
                StoredObjects = new List<ObjectOfAlbertrizal>();
            else
            {
                StoredObjects = storedObjects;

                for (int i = 0; i < StoredObjects.Count; i++)
                {
                    AvailableObjectsList.Items.Add(StoredObjects[i]);
                }
            }

            Title = title;
        }

        protected virtual void AvailableObjectsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            ObjectOfAlbertrizal objectOfAlbertrizal = StoredObjects[listBox.SelectedIndex];

            TargetObjects.Add(objectOfAlbertrizal);
            AddedObjectsList.Items.Add(objectOfAlbertrizal);
        }

        protected virtual void AddedObjectsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            TargetObjects.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
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
                TargetObjects = new List<ObjectOfAlbertrizal>();
        }
    }
}
