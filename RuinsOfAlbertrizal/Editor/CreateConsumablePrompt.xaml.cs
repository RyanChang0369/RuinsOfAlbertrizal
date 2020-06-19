using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Windows;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateConsumablePrompt.xaml
    /// </summary>
    public partial class CreateConsumablePrompt : EditorInterface
    {
        public static Consumable CreatedConsumable { get; set; }

        public CreateConsumablePrompt() : base()
        {
            InitializeComponent();
            DataContext = CreatedConsumable;
            ReloadDefaults();
        }

        public CreateConsumablePrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedConsumable;
            ReloadDefaults();
        }

        protected override void UpdateComponent()
        {
            if (CreatedConsumable == null)
                CreatedConsumable = new Consumable();
        }

        public override void ClearVariable()
        {
            CreatedConsumable = new Consumable();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(ConsumableName);
            RequiredControls.Add(DropChance);
            RequiredControls.Add(HPGain);
            RequiredControls.Add(ManaGain);
            RequiredControls.Add(DefGain);
            RequiredControls.Add(SpdGain);
            RequiredControls.Add(IntGain);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedConsumable.IconLocation = FileHandler.SaveBitmap(CreatedConsumable, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void ReloadDefaults()
        {
            if (Map.DefaultConsumableGuids.Contains(CreatedConsumable.GlobalID))
            {
                DefaultConsumableLbl.Content = "Remove from Default Consumables";
                DefaultConsumableBtn.Content = "Click to Remove";
                DefaultNumberBox.IsEnabled = false;
            }
            else
            {
                DefaultConsumableLbl.Content = "Add to Default Consumables";
                DefaultConsumableBtn.Content = "Click to Add";
                DefaultNumberBox.IsEnabled = true;
            }

        }

        private void DefaultConsumableBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Map.DefaultConsumableGuids.Remove(CreatedConsumable.GlobalID))
            {
                //Consumable not found. Add consumable
                Map.DefaultConsumableGuids.Add(CreatedConsumable.GlobalID);
            }

            ReloadDefaults();
        }
    }
}
