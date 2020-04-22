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
        public static Boss CreatedBoss { get; set; }

        public CreateBossPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedBoss;
        }       

        private void UpdateComponent()
        {
            if (CreatedBoss == null)
                CreatedBoss = new Boss();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        
        //private void RemoveWatermark(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    textBox.Text = "";
        //}

        //private void AddWatermark(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox.Text == "")
        //    {
        //        textBox.Text = "Press Enter to Seperate Lines";
        //    }
        //}
    }
}
