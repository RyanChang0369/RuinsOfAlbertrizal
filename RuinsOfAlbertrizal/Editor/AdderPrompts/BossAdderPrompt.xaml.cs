using RuinsOfAlbertrizal.Characters;
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
    /// Interaction logic for BossAdderPrompt.xaml
    /// </summary>
    public partial class BossAdderPrompt : BaseAdderPrompt
    {
        public List<Boss> TargetBosses { get; set; }

        private List<Boss> OriginalBosses { get; set; }

        public BossAdderPrompt()
        {
            InitializeComponent();
        }

        public BossAdderPrompt(List<Boss> targetBosses)
        {
            InitializeComponent();

            if (targetBosses == null)
            {
                TargetBosses = new List<Boss>();
                OriginalBosses = new List<Boss>();
            }
            else
            {
                TargetBosses = targetBosses;
                OriginalBosses = targetBosses;

                for (int i = 0; i < TargetBosses.Count; i++)
                {
                    AddedBossesList.Items.Add(TargetBosses[i]);
                }
            }
        }

        private void AvailableBossesList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            Boss boss = CreateMapPrompt.Map.StoredBosses[listBox.SelectedIndex];

            LevelSelect levelSelect = new LevelSelect("boss", boss.Name);
            levelSelect.ShowDialog();

            if (levelSelect.GetLevelValue() < 1)
                return;

            boss.Level = levelSelect.GetLevelValue();
            TargetBosses.Add(boss);
            AddedBossesList.Items.Add(boss);
            ListChanged();
        }

        private void AddedBossesList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            TargetBosses.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            ListChanged();
        }

        protected override void ResetVariable()
        {
            TargetBosses = OriginalBosses;
        }
    }
}
