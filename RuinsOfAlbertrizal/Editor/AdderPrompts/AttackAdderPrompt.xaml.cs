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
    /// Interaction logic for AttackAdderPrompt.xaml
    /// </summary>
    public partial class AttackAdderPrompt : Window
    {
        private bool saved = false;

        public List<Attack> TargetAttacks { get; set; }

        public AttackAdderPrompt()
        {
            InitializeComponent();
        }

        public AttackAdderPrompt(List<Attack> targetAttacks)
        {
            InitializeComponent();
            TargetAttacks = targetAttacks;
        }

        private void AvailableAttacksList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            Attack attack = CreateMapPrompt.Map.StoredAttacks[listBox.SelectedIndex];

            TargetAttacks.Add(attack);
            AddedAttacksList.Items.Add(attack);
        }

        private void AddedAttacksList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            TargetAttacks.RemoveAt(listBox.SelectedIndex);
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
            { }
            else if (result == MessageBoxResult.Cancel)
                e.Cancel = true;
            else
                TargetAttacks = null;
        }
    }
}
