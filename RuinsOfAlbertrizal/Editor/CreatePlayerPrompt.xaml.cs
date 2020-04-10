using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreatePlayerPrompt.xaml
    /// </summary>
    public partial class CreatePlayerPrompt : Page
    {
        public static Player CreatedPlayer;
        public CreatePlayerPrompt()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Create(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredTextBoxes = { SpecificName };
            ComboBox[] requiredComboBoxes = { Class };
            //TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            int[] numericalValues = new int[5];

            if (!Validator.Validate(requiredTextBoxes, requiredComboBoxes))
                return;

            switch (Class.SelectedIndex)
            {
                case 0: //Warrior
                    numericalValues[0] = 200;
                    numericalValues[1] = 20;
                    numericalValues[2] = 10;
                    numericalValues[3] = 5;
                    numericalValues[4] = 5;
                    break;
                case 1: //Mage
                    numericalValues[0] = 150;
                    numericalValues[1] = 100;
                    numericalValues[2] = 2;
                    numericalValues[3] = 5;
                    numericalValues[4] = 7;
                    break;
                case 2: //Scout
                    numericalValues[0] = 100;
                    numericalValues[1] = 55;
                    numericalValues[2] = 2;
                    numericalValues[3] = 7;
                    numericalValues[4] = 10;
                    break;
            }

            CreatedPlayer = new Player(GeneralName.Text, SpecificName.Text, Description.Text, numericalValues);
            Back(sender, null);
        }
    }
}
