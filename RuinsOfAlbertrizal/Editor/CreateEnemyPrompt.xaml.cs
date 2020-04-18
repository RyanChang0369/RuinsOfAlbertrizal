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
        public static Enemy CreatedEnemy { get; set; }

        public CreateEnemyPrompt()
        {
            InitializeComponent();
            CreatedEnemy = new Enemy();
            DataContext = CreatedEnemy;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            //TextBox[] requiredTextBoxes = { SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            //TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            //int[] numericalValues = new int[5];

            //for (int i = 0; i < numericalBoxes.Length; i++)
            //{
            //    numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
            //    numericalValues[i] = int.Parse(numericalBoxes[i].Text);
            //}

            //if (!Validator.ValidateTextBoxes(requiredTextBoxes, null))
            //    return;

            //CreatedEnemy = new Enemy(GeneralName.Text, SpecificName.Text, Description.Text, numericalValues);
            Back(sender, null);
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Enemy temp = (Enemy)FileHandler.LoadObject();

            //    if (temp == null)
            //        return;

            //    CreatedEnemy = temp;

            //    SpecificName.Text = temp.SpecificName;
            //    GeneralName.Text = temp.GeneralName;
            //    Description.Text = temp.Description;

            //    BaseHP.Text = temp.BaseStats[0] + "";
            //    BaseMana.Text = temp.BaseStats[1] + "";
            //    BaseDef.Text = temp.BaseStats[2] + "";
            //    BaseDmg.Text = temp.BaseStats[3] + "";
            //    BaseSpd.Text = temp.BaseStats[4] + "";
            //}
            //catch (Exception)
            //{
            //    return;
            //}
        }
    }
}
