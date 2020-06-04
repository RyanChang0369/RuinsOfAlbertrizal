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
using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.XMLInterpreter;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateItemPrompt.xaml
    /// </summary>
    public partial class CreateEquiptmentPrompt : EditorInterface
    {
        public static Equiptment CreatedEquiptment { get; set; }

        public CreateEquiptmentPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedEquiptment;
        }

        protected override void UpdateComponent()
        {
            if (CreatedEquiptment == null)
                CreatedEquiptment = new Equiptment();
        }

        public override void ClearVariable()
        {
            CreatedEquiptment = new Equiptment();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(EquiptmentName);
        }

        private void SelectSlotsBtn_Click(object sender, RoutedEventArgs e)
        {
            SlotAdderPrompt slotAdderPrompt = new SlotAdderPrompt(CreatedEquiptment.Slots);
            slotAdderPrompt.ShowDialog();
            CreatedEquiptment.Slots = slotAdderPrompt.Slots;
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedEquiptment.IconLocation = FileHandler.SaveBitmap(CreatedEquiptment, "icon");
            }
            catch (ArgumentException)
            {

            }
        }
    }
}
