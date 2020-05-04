using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateBossPrompt.xaml
    /// </summary>
    public partial class CreateBossPrompt : EditorInterface
    {
        public static Boss CreatedBoss { get; set; }

        public CreateBossPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedBoss;
        }       

        protected override void UpdateComponent()
        {
            if (CreatedBoss == null)
                CreatedBoss = new Boss();
        }

        protected override void ClearVariable()
        {
            CreatedBoss = new Boss();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(BossName);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedBoss.IconLocation = GetBitmapPath();
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedBoss.WorldImgLocation = GetBitmapPath();
        }
    }
}
