using RuinsOfAlbertrizal.Editor.AdderPrompts;
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
    /// Interaction logic for CreateAttackPrompt.xaml
    /// </summary>
    public partial class CreateAttackPrompt : EditorInterface
    {
        public static Attack CreatedAttack { get; set; }

        public CreateAttackPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedAttack;
        }

        protected override void UpdateComponent()
        {
            if (CreatedAttack == null)
                CreatedAttack = new Attack();
        }

        public override void ClearVariable()
        {
            CreatedAttack = new Attack();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(AttackName);
        }
    }
}
