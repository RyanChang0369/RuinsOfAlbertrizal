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
using static RuinsOfAlbertrizal.Items.Equiptment;

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    /// <summary>
    /// Interaction logic for SlotAdderPrompt.xaml
    /// </summary>
    public partial class SlotAdderPrompt : Window
    {
        public bool EquiptableSlotsEnabled = true;

        public List<SlotMode> EquiptableSlots { get; set; }

        public List<SlotMode> RequiredSlots { get; set; }

        public SlotAdderPrompt()
        {
            InitializeComponent();
            UpdateComponent();
        }

        public SlotAdderPrompt(List<SlotMode> equiptableSlots, List<SlotMode> requiredSlots)
        {
            InitializeComponent();
            EquiptableSlots = equiptableSlots;
            RequiredSlots = requiredSlots;
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            List<CheckBox> equiptableCheckBoxes = EquiptableSlotsContainer.Children.OfType<CheckBox>().ToList();
            List<CheckBox> requiredCheckBox = RequiredSlotsContainer.Children.OfType<CheckBox>().ToList();
            
            for (int i = 0; i < EquiptableSlots.Count; i++)
            {
                equiptableCheckBoxes[(int)EquiptableSlots[i] - 1].IsChecked = true;
            }

            for (int i = 0; i < RequiredSlots.Count; i++)
            {
                requiredCheckBox[(int)RequiredSlots[i] - 1].IsChecked = true;
            }
        }

        private void SelectSlot_Clicked(object sender, RoutedEventArgs e)
        {
            List<CheckBox> equiptableCheckBoxes = EquiptableSlotsContainer.Children.OfType<CheckBox>().ToList();
            List<CheckBox> requiredCheckBox = RequiredSlotsContainer.Children.OfType<CheckBox>().ToList();

            for (int i = 0; i < equiptableCheckBoxes.Count; i++)
            {
                if (equiptableCheckBoxes[i].IsChecked == true && !EquiptableSlots.Contains((SlotMode)(i + 1)))
                {
                    EquiptableSlots.Add((SlotMode)(i + 1));
                }
            }

            for (int i = 0; i < requiredCheckBox.Count; i++)
            {
                if (requiredCheckBox[i].IsChecked == true && !RequiredSlots.Contains((SlotMode)(i + 1)))
                {
                    RequiredSlots.Add((SlotMode)(i + 1));
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Help1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Equiptable slots are slots that the item is equipted to.\r\n\r\n" +
                "Required slots are slots that the equiptment automatically takes up " +
                "when equipting.\r\n\r\nNote that the slot selected by the player will also " +
                "be taken up.");
        }

        private void SlotTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (EquiptableSlotsEnabled)
            {
                btn.Content = "Editing Required Slots";
                Panel.SetZIndex(RequiredSlotsContainer, 2);
                Panel.SetZIndex(EquiptableSlotsContainer, 1);
            }
            else
            {
                btn.Content = "Editing Equiptable Slots";
                Panel.SetZIndex(RequiredSlotsContainer, 1);
                Panel.SetZIndex(EquiptableSlotsContainer, 2);
            }

            EquiptableSlotsEnabled = !EquiptableSlotsEnabled;
        }
    }
}
