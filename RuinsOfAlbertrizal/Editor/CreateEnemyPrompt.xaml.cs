using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CreateEnemyPrompt.xaml
    /// </summary>
    public partial class CreateEnemyPrompt : EditorInterface
    {
        public static Enemy CreatedEnemy { get; set; }
        public CreateEnemyPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedEnemy;
        }

        protected override void UpdateComponent()
        {
            if (CreatedEnemy == null)
                CreatedEnemy = new Enemy();
        }

        public override void ClearVariable()
        {
            CreatedEnemy = new Enemy();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(EnemyName);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedEnemy.IconLocation = FileHandler.SaveBitmap(CreatedEnemy, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedEnemy.WorldImgLocation = FileHandler.SaveBitmap(CreatedEnemy, "icon");
            }
            catch (ArgumentException)
            {

            }
        }
    }
}
