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

        public BuffAdderPrompt()
        {
            InitializeComponent();
        }

        public BuffAdderPrompt(List<Buff> targetBuffs)
        {
            InitializeComponent();
            TargetBuffs = targetBuffs;
        }

        private void ListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            Buff buff = CreateMapPrompt.Map.StoredBuffs[listBox.SelectedIndex];

            LevelSelect levelSelect = new LevelSelect("buff", buff.Name);
            levelSelect.ShowDialog();

            buff.Level = levelSelect.GetLevelValue();
            TargetBuffs.Add(buff);
            AddedBuffsList.Items.Add(buff);
        }

        private void ListBox_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            TargetBuffs.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }
    }
}
