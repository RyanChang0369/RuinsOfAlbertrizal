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
    /// Interaction logic for BuffAdderPrompt.xaml
    /// </summary>
    public partial class BuffAdderPrompt : Window
    {
        public List<Buff> TargetBuffs { get; set; }

        private bool saved = false;

        public BuffAdderPrompt()
        {
            InitializeComponent();
        }

        public BuffAdderPrompt(List<Buff> targetBuffs)
        {
            InitializeComponent();

            if (targetBuffs == null)
                TargetBuffs = new List<Buff>();
            else
            {
                TargetBuffs = targetBuffs;

                for (int i = 0; i < TargetBuffs.Count; i++)
                {
                    AddedBuffsList.Items.Add(TargetBuffs[i]);
                }
            }
        }

        private void AvailableBuffsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            Buff buff = CreateMapPrompt.Map.StoredBuffs[listBox.SelectedIndex];

            LevelSelect levelSelect = new LevelSelect("buff", buff.Name);
            levelSelect.ShowDialog();

            if (levelSelect.GetLevelValue() < 1)
                return;

            buff.Level = levelSelect.GetLevelValue();
            TargetBuffs.Add(buff);
            AddedBuffsList.Items.Add(buff);
        }

        private void AddedBuffsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            TargetBuffs.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            saved = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (saved)
                return;

            MessageBoxResult result = MessageBox.Show("Save before quitting?", "Unsaved Work", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {  }
            else if (result == MessageBoxResult.Cancel)
                e.Cancel = true;
            else
                TargetBuffs = null;
        }
    }
}
