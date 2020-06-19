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
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.XMLInterpreter;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateItemPrompt.xaml
    /// </summary>
    public partial class CreateItemPrompt : EditorInterface
    {
        public static Item CreatedItem { get; set; }

        public CreateItemPrompt() : base()
        {
            InitializeComponent();
            DataContext = CreatedItem;
            ReloadDefaults();
        }

        public CreateItemPrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedItem;
            ReloadDefaults();
        }

        protected override void UpdateComponent()
        {
            if (CreatedItem == null)
                CreatedItem = new Item();
        }

        public override void ClearVariable()
        {
            CreatedItem = new Item();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(ItemName);
            RequiredControls.Add(DropChance);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedItem.IconLocation = FileHandler.SaveBitmap(CreatedItem, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void ReloadDefaults()
        {
            if (Map.DefaultItemGuids.Contains(CreatedItem.GlobalID))
            {
                DefaultItemLbl.Content = "Remove from Default Items";
                DefaultItemBtn.Content = "Click to Remove";
                DefaultNumberBox.IsEnabled = false;
            }
            else
            {
                DefaultItemLbl.Content = "Add to Default Items";
                DefaultItemBtn.Content = "Click to Add";
                DefaultNumberBox.IsEnabled = true;
            }

        }

        private void DefaultItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Map.DefaultItemGuids.Remove(CreatedItem.GlobalID))
            {
                //Item not found. Add Item
                Map.DefaultItemGuids.Add(CreatedItem.GlobalID);
            }

            ReloadDefaults();
        }
    }
}
