using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Items;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RuinsOfAlbertrizal.Inventory
{
    /// <summary>
    /// Interaction logic for FloatingInventory.xaml
    /// </summary>
    public partial class FloatingInventory : BaseWindow
    {
        public Player SelectedPlayer;

        public Item SelectedItem;

        private int turnsPassed = 0;

        public int TurnsPassed
        {
            get => turnsPassed;
            set
            {
                turnsPassed = value;

                if (turnsPassed >= GameBase.NumTurns)
                    Close();
                else
                    TurnsLeftLbl.Content = $"Turns Left: {GameBase.NumTurns - turnsPassed}";
            }
        }

        public bool AllTurnsDone => TurnsPassed >= GameBase.NumTurns;

        public FloatingInventory(Player player)
        {
            SelectedPlayer = player;
            DataContext = SelectedPlayer;
            TurnsPassed = 0;
            InitializeComponent();
        }

        public FloatingInventory(Player player, int turnsPassed)
        {
            SelectedPlayer = player;
            DataContext = SelectedPlayer;
            InitializeComponent();
            TurnsPassed = turnsPassed;
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
                //All checks successful. Equipt equiptment.

                SelectedPlayer.Equipt((Equiptment)SelectedItem, (Equiptment.SlotMode)(index + 1));
                SelectedItem = null;
                TurnsPassed++;
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
            TurnsPassed++;
        }
    }
}
