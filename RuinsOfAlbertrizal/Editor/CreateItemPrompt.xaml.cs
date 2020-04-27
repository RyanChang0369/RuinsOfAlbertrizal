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
    public partial class CreateItemPrompt : EditorInterface
    {
        public Item CreatedItem { get; set; }

        public CreateItemPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedItem;
        }

        protected override void UpdateComponent()
        {
            if (CreatedItem == null)
                CreatedItem = new Item();
        }

        protected override bool FormIsValid()
        {
            return Form.IsValid();
        }

        protected override void ClearVariable()
        {
            CreatedItem = new Item();
        }
    }
}
