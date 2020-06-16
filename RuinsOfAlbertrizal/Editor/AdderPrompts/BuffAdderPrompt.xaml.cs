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
    public partial class BuffAdderPrompt : BaseAdderPrompt
    {
        private List<Buff> TargetBuffs { get; set; }

        private List<Buff> OriginalBuffs { get; set; }

        public List<Buff> GetSelected()
        {
            return TargetBuffs.MemoryClone();
        }

        public BuffAdderPrompt()
        {
            InitializeComponent();
        }

        public BuffAdderPrompt(List<Buff> targetBuffs)
        {
            InitializeComponent();

            if (targetBuffs == null)
            {
                TargetBuffs = new List<Buff>();
                OriginalBuffs = new List<Buff>();
            }
            else
            {
                TargetBuffs = targetBuffs;
                OriginalBuffs = targetBuffs;

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
            ListChanged();
        }

        private void AddedBuffsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            TargetBuffs.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            ListChanged();
        }

        protected override void ResetVariable()
        {
            TargetBuffs = OriginalBuffs;
        }
    }
}
