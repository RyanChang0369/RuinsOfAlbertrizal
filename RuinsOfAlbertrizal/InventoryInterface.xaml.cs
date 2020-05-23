using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
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
        public Item SelectedItem;

        public ItemInterface()
        {
            InitializeComponent();
            DataContext = PartyMembersInterface.SelectedPlayer;
            //UpdateInventoryGrid(PartyMembersInterface.SelectedPlayer);
        }

        private void UpdateInventoryGrid(Player player)
        {
            InventoryGrid.Children.Clear();

            Grid container = new Grid();

            int row = 0;
            int col = 0;

            int maxRows = InventoryGrid.RowDefinitions.Count;
            int maxCols = InventoryGrid.ColumnDefinitions.Count;
            int numStoredItems = player.InventoryConsumables.Count + player.InventoryEquiptments.Count + player.InventoryItems.Count;
            int numPages = (int)Math.Ceiling((double)numStoredItems / (maxRows * maxCols));

            //Create a tab control to store the items in
            TabControl inventoryTabControl = new TabControl();

            for (int i = 0; i < numPages; i++)
            {
                TabItem tabItem = new TabItem
                {
                    Header = $"Page {i + 1}"
                };
            }

            //For one page, create an array with all the checkboxes
            for (int i = 0; i < maxRows * maxCols; i++)
            {
                if (col == maxCols)
                {
                    col = 0;
                    row++;
                }

                CheckBox checkBox = new CheckBox();
                checkBox.SetValue(Grid.RowProperty, row);
                checkBox.SetValue(Grid.ColumnProperty, col);

                col++;

                try
                {
                    checkBox.ToolTip = player.InventoryEquiptments[i];
                    checkBox.Tag = player.InventoryEquiptments[i];
                    continue;
                }
                catch (Exception)
                {

                }

                int invIndex = player.InventoryEquiptments.Count + i;

                try
                {
                    checkBox.ToolTip = player.InventoryConsumables[invIndex];
                    checkBox.Tag = player.InventoryConsumables[invIndex];
                    continue;
                }
                catch (Exception)
                {

                }

                invIndex += player.InventoryConsumables.Count;

                try
                {
                    checkBox.ToolTip = player.InventoryItems[invIndex];
                    checkBox.Tag = player.InventoryItems[invIndex];
                    continue;
                }
                catch (Exception)
                {

                }
            }
        }

        private void InventoryCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
                return;


        }

        private void InventoryTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            SelectedItem = (Item)textBlock.DataContext;
        }
    }
}
