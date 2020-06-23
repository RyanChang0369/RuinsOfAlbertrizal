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
using RuinsOfAlbertrizal.Environment;
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

        public CreateEquiptmentPrompt() : base()
        {
            InitializeComponent();
            DataContext = CreatedEquiptment;
        }

        public CreateEquiptmentPrompt(Map map) : base(map)
        {
            InitializeComponent();
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
            RequiredControls.Add(HPGain);
            RequiredControls.Add(ManaGain);
            RequiredControls.Add(DefGain);
            RequiredControls.Add(SpdGain);
            RequiredControls.Add(IntGain);
        }

        private void SelectSlotsBtn_Click(object sender, RoutedEventArgs e)
        {
            SlotAdderPrompt slotAdderPrompt = new SlotAdderPrompt(CreatedEquiptment.EquiptableSlots, CreatedEquiptment.RequiredSlots);
            slotAdderPrompt.ShowDialog();
            CreatedEquiptment.EquiptableSlots = slotAdderPrompt.EquiptableSlots;
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
