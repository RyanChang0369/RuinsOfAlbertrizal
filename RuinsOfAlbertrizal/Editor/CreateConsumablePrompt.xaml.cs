using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.XMLInterpreter;
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

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateConsumablePrompt.xaml
    /// </summary>
    public partial class CreateConsumablePrompt : EditorInterface
    {
        public static Consumable CreatedConsumable { get; set; }

        public CreateConsumablePrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedConsumable;
        }

        protected override void UpdateComponent()
        {
            if (CreatedConsumable == null)
                CreatedConsumable = new Consumable();
        }

        protected override void ClearVariable()
        {
            CreatedConsumable = new Consumable();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(ConsumableName);
            RequiredControls.Add(DropChance);
        }

        private void EditBuffBtn_Click(object sender, RoutedEventArgs e)
        {
            BuffAdderPrompt buffAdderPrompt = new BuffAdderPrompt(CreatedConsumable.Buffs);
            buffAdderPrompt.ShowDialog();
            CreatedConsumable.Buffs = buffAdderPrompt.TargetBuffs;
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedConsumable.IconLocation = FileHandler.SaveBitmap(CreatedConsumable, "icon");
        }
    }
}
