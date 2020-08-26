using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
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

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PartySlotSelector.xaml
    /// </summary>
    public partial class PartySlotSelector : Window
    {
        private Map Map { get; set; }

        private Player SelectedPlayer { get; set; }

        private Border SelectedBorder { get; set; }

        public PartySlotSelector()
        {
            InitializeComponent();
        }

        public PartySlotSelector(Map map)
        {
            InitializeComponent();
            Map = map;
            DataContext = map;
        }

        private void Party_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;

            if (border.Tag == null)
            {
                border.Tag = "borderSelected";
                border.BorderBrush = new SolidColorBrush(Colors.Red);
                SelectedPlayer = (Player)border.DataContext;
                SelectedBorder = border;
                SelectAllSlots();
            }
            else
            {
                border.Tag = null;
                border.BorderBrush = new SolidColorBrush(Colors.Transparent);
                SelectedPlayer = null;
                SelectedBorder = null;
                DeselectAllSlots();
            }
        }

        private void DeselectAllParty()
        {
            if (SelectedBorder != null)
            {
                SelectedBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
                SelectedBorder.Tag = null;
            }
        }

        private void SelectAllSlots()
        {
            foreach (Label label in SlotGrid.Children.OfType<Label>())
            {
                label.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            }
        }

        private void DeselectAllSlots()
        {
            foreach (Label label in SlotGrid.Children.OfType<Label>())
            {
                label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void Slot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;

            if (SelectedPlayer != null)
            {
                int selectedIndex = int.Parse((string)lbl.Tag);
                Map.ActivePlayerGuids[selectedIndex] = SelectedPlayer.GlobalID;

                lbl.Content = new Image
                {
                    Source = SelectedPlayer.IconAsBitmapSource
                };

                SelectedPlayer = null;
                DeselectAllParty();
            }
            else
            {
                lbl.Content = $"Slot {(int)lbl.Tag + 1}\r\n[Empty]";
            }

            DeselectAllSlots();
        }

        private void Slot_Drop(object sender, DragEventArgs e)
        {
            Slot_MouseDown(sender, null);
        }
    }
}
