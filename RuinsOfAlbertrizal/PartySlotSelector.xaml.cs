using RuinsOfAlbertrizal.Characters;
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
        public int SelectedIndex { get; set; }

        public PartySlotSelector()
        {
            InitializeComponent();
        }

        public PartySlotSelector(Player selectedPlayer, Player[] activePlayers)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            SelectedIndex = int.Parse((string)btn.Tag);
            Close();
        }
    }
}
