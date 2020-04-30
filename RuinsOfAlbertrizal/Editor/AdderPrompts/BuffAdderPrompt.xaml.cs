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

            LevelSelect levelSelect = new LevelSelect("buff", CreateMapPrompt.Map.StoredBuffs[listBox.SelectedIndex].Name);
            levelSelect.ShowDialog();

            CreateMapPrompt.Map.StoredBuffs[listBox.SelectedIndex].Level = levelSelect.GetLevelValue();
        }
    }
}
