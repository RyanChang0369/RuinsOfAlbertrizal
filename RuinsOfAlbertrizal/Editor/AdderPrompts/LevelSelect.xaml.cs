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
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    /// <summary>
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
        public int GetLevelValue()
        {
            try
            {
                return int.Parse(ValueTextBox.Text);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        public LevelSelect()
        {
            InitializeComponent();
        }

        public LevelSelect(string objectType, string objectName)
        {
            InitializeComponent();
            this.Title = $"Select a {objectType} level for {objectName}.";
            HeaderLabel.Content = $"Select a {objectType} level for {objectName}.";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (GetLevelValue() < 1)
            {
                MessageBoxResult result = MessageBox.Show("Level must be 1 or higher.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                if (result == MessageBoxResult.OK)
                    return;
            }
            this.Close();
        }
    }
}
