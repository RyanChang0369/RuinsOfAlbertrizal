using System;
using System.Windows;
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
        }

        public CreateItemPrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedItem;
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
    }
}
