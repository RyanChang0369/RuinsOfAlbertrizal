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
    /// Interaction logic for EditorInterface.xaml
    /// </summary>
    public partial class EditorInterface : Page
    {
        public static EditorProperties EditorProperties;
        public EditorInterface()
        {
            InitializeComponent();
            ConstructInterface();
        }

        public void ConstructInterface()
        {
            Form.Children.Clear();
            Title = EditorProperties.Title;
            TitleLabel.Content = EditorProperties.Title;

            foreach (TextBox textBox in EditorProperties.RequiredTextBoxes)
            {
                Form.Children.Add(textBox);
            }

            foreach (TextBox textBox in EditorProperties.NumericalTextBoxes)
            {
                Form.Children.Add(textBox);
            }

            foreach (ComboBox comboBox in EditorProperties.RequiredComboBoxes)
            {
                Form.Children.Add(comboBox);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            TextBox[] requiredTextBoxes = EditorProperties.RequiredTextBoxes;
            TextBox[] numericalBoxes = EditorProperties.NumericalTextBoxes;
            int[] numericalValues = new int[5];

            for (int i = 0; i < numericalBoxes.Length; i++)
            {
                numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
                numericalValues[i] = int.Parse(numericalBoxes[i].Text);
            }

            if (!Validator.ValidateTextBoxes(requiredTextBoxes, null))
                return;
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
