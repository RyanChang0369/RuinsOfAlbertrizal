using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Mechanics;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for CreateHazardPrompt.xaml
    /// </summary>
    public partial class CreateHazardPrompt : EditorInterface
    {
        public static Hazard CreatedHazard { get; set; }
        public CreateHazardPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedHazard;
        }

        protected override void UpdateComponent()
        {
            if (CreatedHazard == null)
                CreatedHazard = new Hazard();
        }

        public override void ClearVariable()
        {
            CreatedHazard = new Hazard();
        }

        private void SelectTileImageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedHazard.TileImageLocation = FileHandler.SaveBitmap(CreatedHazard, "tile");
            }
            catch (ArgumentException)
            {

            }
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(HazardName);
        }
    }
}
