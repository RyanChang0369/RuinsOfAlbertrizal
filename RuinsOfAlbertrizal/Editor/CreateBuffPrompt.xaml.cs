using RuinsOfAlbertrizal.Mechanics;
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
    /// Interaction logic for CreateBuffPrompt.xaml
    /// </summary>
    public partial class CreateBuffPrompt : EditorInterface
    {
        public static Buff CreatedBuff { get; set; }
        public CreateBuffPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedBuff;
        }

        protected override void UpdateComponent()
        {
            if (CreatedBuff == null)
                CreatedBuff = new Buff();
        }

        protected override void ClearVariable()
        {
            CreatedBuff = new Buff();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(BuffName);
        }
    }
}
