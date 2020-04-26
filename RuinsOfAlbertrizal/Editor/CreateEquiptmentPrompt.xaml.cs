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
using RuinsOfAlbertrizal.Items;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateItemPrompt.xaml
    /// </summary>
    public partial class CreateEquiptmentPrompt : EditorInterface
    {
        public Equiptment CreatedEquiptment { get; set; }

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

        protected override bool FormIsValid()
        {
            return Form.IsValid();
        }
    }
}
