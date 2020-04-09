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
    public partial class CreateEnemyPrompt : Page
    {
        public static Enemy CreatedEnemy;
        public CreateEnemyPrompt()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/EditorMenu.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredTextBoxes = { GeneralName, SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            int[] numericalValues = new int[5];

            if (!Validator.Validate(requiredTextBoxes, null))
                return;

            for (int i = 0; i < numericalBoxes.Length; i++)
            {
                numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
                numericalValues[i] = int.Parse(numericalBoxes[i].Text);
            }

            CreatedEnemy = new Enemy(GeneralName.Text, SpecificName.Text, numericalValues);
        }
    }
}
