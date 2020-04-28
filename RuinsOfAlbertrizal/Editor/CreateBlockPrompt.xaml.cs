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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateBlockPrompt.xaml
    /// </summary>
    public partial class CreateBlockPrompt : EditorInterface
    {
        public static Block CreatedBlock { get; set; }
        public CreateBlockPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedBlock;
        }

        protected override void UpdateComponent()
        {
            if (CreatedBlock == null)
                CreatedBlock = new Block();
        }

        protected override bool FormIsValid()
        {
            return Form.IsValid();
        }

        protected override void ClearVariable()
        {
            CreatedBlock = new Block();
        }

        private void SelectTileImageBtn_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new FileDialog((int)FileDialog.DialogOptions.Open, "PNG File | .png");

            try
            {
                CreatedBlock.TileImage = (Bitmap)Bitmap.FromFile(dialog.GetPath());
            }
            catch (ArgumentNullException)
            {

            }
            catch (IOException)
            {

            }
        }
    }
}
