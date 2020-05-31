﻿using System;
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
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
