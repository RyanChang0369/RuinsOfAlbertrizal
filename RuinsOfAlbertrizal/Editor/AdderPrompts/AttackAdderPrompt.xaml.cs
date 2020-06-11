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
    public partial class AttackAdderPrompt : BaseAdderPrompt
    {
        public List<Attack> TargetAttacks { get; set; }

        public List<Attack> OriginalAttacks { get; set; }

        public AttackAdderPrompt()
        {
            InitializeComponent();
        }

        public AttackAdderPrompt(List<Attack> targetAttacks)
        {
            InitializeComponent();
            TargetAttacks = targetAttacks;
            OriginalAttacks = targetAttacks;
        }

        private void AvailableAttacksList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            Attack attack = CreateMapPrompt.Map.StoredAttacks[listBox.SelectedIndex];

            TargetAttacks.Add(attack);
            AddedAttacksList.Items.Add(attack);
            ListChanged();
        }

        private void AddedAttacksList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            TargetAttacks.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            ListChanged();
        }

        protected override void ResetVariable()
        {
            TargetAttacks = OriginalAttacks;
        }
    }
}
