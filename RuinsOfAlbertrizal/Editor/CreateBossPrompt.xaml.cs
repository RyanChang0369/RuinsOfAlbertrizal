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
    public partial class CreateBossPrompt : Page
    {
        public static Boss CreatedBoss;

        public CreateBossPrompt()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredTextBoxes = { SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            int[] numericalValues = Validator.ParseNumericalValues(numericalBoxes);

            Boss temp = new Boss(GeneralName.Text, SpecificName.Text, Description.Text, numericalValues,
                BossMessageStart.Text.Split('\n'), BossMesageDefeat.Text.Split('\n'), BossMessageVictory.Text.Split('\n'));

            if (!Validator.ValidateTextBoxes(requiredTextBoxes, null))
                return;

            CreatedBoss = temp;
            Back(sender, null);
        }
        private void Create(object sender, RoutedEventArgs e)
        {

        }
        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                Boss temp = (Boss)FileHandler.LoadObject();

                if (temp == null)
                    return;

                CreatedBoss = temp;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void RemoveWatermark(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = "";
        }

        private void AddWatermark(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = "Press Enter to Seperate Lines";
            }
        }
    }
}
