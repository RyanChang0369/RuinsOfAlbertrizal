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
        }

        public CreateConsumablePrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedConsumable;
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
    }
}
