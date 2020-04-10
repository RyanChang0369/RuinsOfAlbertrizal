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
        private void Create(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredTextBoxes = { SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            int[] numericalValues = new int[5];

            for (int i = 0; i < numericalBoxes.Length; i++)
            {
                numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
                numericalValues[i] = int.Parse(numericalBoxes[i].Text);
            }

            if (!Validator.Validate(requiredTextBoxes, null))
                return;

            CreatedBoss = new Boss(GeneralName.Text, SpecificName.Text, Description.Text, numericalValues,
                BossMessageStart.Text.Split('\n'), BossMesageDefeat.Text.Split('\n'), BossMessageVictory.Text.Split('\n'));
            Back(sender, null);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Boss temp = (Boss)FileHandler.LoadObject();

            if (temp == null)
                return;

            CreatedBoss = temp;
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
