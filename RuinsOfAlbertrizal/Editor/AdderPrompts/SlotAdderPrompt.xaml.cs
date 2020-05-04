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
        public List<SlotMode> Slots { get;set; }

        public SlotAdderPrompt()
        {
            InitializeComponent();
            UpdateComponent();
        }

        public SlotAdderPrompt(List<SlotMode> slots)
        {
            InitializeComponent();
            Slots = slots;
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            List<CheckBox> checkBoxes = SlotsContainer.Children.OfType<CheckBox>().ToList();
            for (int i = 0; i < Slots.Count; i++)
            {
                checkBoxes[(int)Slots[i] - 1].IsChecked = true;
            }
        }

        private void SelectSlot_Clicked(object sender, RoutedEventArgs e)
        {
            List<CheckBox> checkBoxes = SlotsContainer.Children.OfType<CheckBox>().ToList();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].IsChecked == true && !Slots.Contains((SlotMode)(i + 1)))
                {
                    Slots.Add((SlotMode)(i + 1));
                }
            }

            //switch (checkBox.Name)
            //{
            //    case "HeadSlot":
            //        slot = SlotMode.Head;
            //        break;
            //    case "NeckSlot":
            //        slot = SlotMode.Neck;
            //        break;
            //    case "TorsoSlot":
            //        slot = SlotMode.Torso;
            //        break;
            //    case "LeftHandSlot":
            //        slot = SlotMode.Hand1;
            //        break;
            //    case "RightHandSlot":
            //        slot = SlotMode.Hand2;
            //        break;
            //    case "LegsSlot":
            //        slot = SlotMode.Legs;
            //        break;
            //    case "FeetSlot":
            //        slot = SlotMode.Feet;
            //        break;
            //}

            //// Is selected. Add slot.
            //if (checkBox.IsChecked == true)
            //{
            //    Slots.Add(slot);
            //}
            //else
            //{
            //    Slots.Remove(slot);
            //}
        }

        /// <summary>
        /// Returns false if sender is not selected
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private bool ToggleButton(Button sender)
        {
            if (sender.Tag == null || (string)sender.Tag == "")
            {
                sender.Background = new SolidColorBrush(Colors.Gray);
                sender.Tag = "Selected";
                return false;
            }
            else
            {
                sender.Background = new SolidColorBrush(Colors.LightGray);
                sender.Tag = "";
                return true;
            }
        }
    }
}
