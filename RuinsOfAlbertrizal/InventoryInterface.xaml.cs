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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for ItemInterface.xaml
    /// </summary>
    public partial class ItemInterface : Page
    {
        public ItemInterface()
        {
            InitializeComponent();
            UpdateInventoryGrid(PartyMembersInterface.SelectedPlayer);
        }

        public void UpdateInventoryGrid(Player player)
        {
            InventoryGrid.Children.Clear();

            Grid container = new Grid();

            int row = 0;
            int col = 0;

            int maxRows = InventoryGrid.RowDefinitions.Count;
            int maxCols = InventoryGrid.ColumnDefinitions.Count;

            for (int i = 0; i < maxRows * maxCols; i++)
            {
                if (col == maxCols)
                {
                    col = 0;
                    row++;
                }

                //Finish

                col++;
            }
        }
    }
}
