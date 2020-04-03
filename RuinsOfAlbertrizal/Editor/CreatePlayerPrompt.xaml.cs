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
        public CreatePlayerPrompt()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/EditorMenu.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredBoxes = { GeneralName, SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            int[] numericalValues = new int[5];

            for (int i = 0; i < numericalBoxes.Length; i++)
            {
                numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
                numericalValues[i] = int.Parse(numericalBoxes[i].Text);
            }

            foreach (TextBox box in requiredBoxes)
            {
                if (box.Text == null || box.Text == "")
                {
                    MessageBox.Show("Please fill out all required text boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            FileDialog dialog = new FileDialog((int)FileDialog.DialogOptions.Save);

            Player player = new Player(GeneralName.Text, SpecificName.Text, numericalValues);

            try
            {
                FileHandler.SavePlayer(player, dialog.Path);
            }
            catch (IOException)
            {
                MessageBox.Show("File not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
