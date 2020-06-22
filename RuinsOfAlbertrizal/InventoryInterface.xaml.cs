using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Text;
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
    /// Interaction logic for InventoryInterface.xaml
    /// </summary>
    public partial class InventoryInterface : BasePage
    {
        public Player SelectedPlayer;

        public Item SelectedItem;

        public InventoryInterface()
        {
            SelectedPlayer = PartyMembersInterface.SelectedPlayer;
            DataContext = SelectedPlayer;
            InitializeComponent();
        }

        public InventoryInterface(Player player)
        {
            SelectedPlayer = player;
            DataContext = SelectedPlayer;
            InitializeComponent();
        }

        private void InventoryCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = Array.IndexOf(SlotsContainer.Children.OfType<Button>().ToArray(), button);

            if (SelectedItem == null && button.Content != null)
            {
                SelectedPlayer.Unequipt(index);
            }
            else if (SelectedItem == null) //Nothing selected and button.Content is null
            {
                MessageBox.Show("To equipt a piece of equiptment, click on an equiptment in the Equiptment tab and click on the appropriate slot.");
                return;
            }
            else if (SelectedItem.GetType() != typeof(Equiptment))
                return;
            else if (!((Equiptment)SelectedItem).EquiptableSlots.Contains((Equiptment.SlotMode)(index + 1)))
            {
                //Equiptment does not fit on specified slots. Generate error message.

                string selectedItemSlotsList = "";

                for (int i = 0; i < ((Equiptment)SelectedItem).EquiptableSlots.Count; i++)
                {
                    Equiptment.SlotMode slot = ((Equiptment)SelectedItem).EquiptableSlots[i];

                    selectedItemSlotsList = $"{selectedItemSlotsList}" +
                        $"{MiscMethods.GetSeperator(i, ((Equiptment)SelectedItem).EquiptableSlots.Count)} " +
                        $"{slot.GetDescription()}".Trim();
                }

                MessageBox.Show($"The {SelectedItem.Name} only fits on slots {selectedItemSlotsList}.");
                return;
            }
            else
            {
                //All test successful. Equipt equiptment.

                SelectedPlayer.Equipt((Equiptment)SelectedItem, (Equiptment.SlotMode)(index + 1));
                SelectedItem = null;
            }

            foreach (Button btn in SlotsContainer.Children.OfType<Button>().ToArray())
            {
                btn.GetBindingExpression(Button.ContentProperty).UpdateTarget();
            }

            ForceListBoxUpdate(equiptmentsList);
        }

        private void ForceInventoryUpdate()
        {
            ForceListBoxUpdate(equiptmentsList);
            ForceListBoxUpdate(consumablesList);
            ForceListBoxUpdate(itemsList);
        }

        private void EquiptmentStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            SelectedItem = (Item)element.DataContext;
        }

        private void ConsumableStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            EquiptmentStackPanel_MouseUp(sender, e);

            string message = $"You will gain the following buffs from eating the {SelectedItem.Name}: \r\n";

            for (int i = 0; i < ((Consumable)SelectedItem).Buffs.Count; i++)
            {
                message = $"{message}{MiscMethods.GetSeperator(i, ((Consumable)SelectedItem).Buffs.Count)} " +
                    $"{((Consumable)SelectedItem).Buffs[i].DisplayName}";
            }

            MessageBoxResult result = MessageBox.Show(message, "", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.Cancel)
                return;

            SelectedPlayer.Consume((Consumable)SelectedItem);
            ForceListBoxUpdate(consumablesList);
        }
    }
}
