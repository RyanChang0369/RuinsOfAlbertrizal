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
            CreatedBoss = new Boss();
            DataContext = CreatedBoss;
        }       

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            //TextBox[] requiredTextBoxes = { SpecificName, BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            //TextBox[] numericalBoxes = { BaseHP, BaseMana, BaseDef, BaseDmg, BaseSpd };
            //int[] numericalValues = Validator.ParseNumericalValues(numericalBoxes);

            //Boss temp = new Boss(GeneralName.Text, SpecificName.Text, Description.Text, numericalValues,
            //    BossMessageStart.Text.Split('\n'), BossMesageDefeat.Text.Split('\n'), BossMessageVictory.Text.Split('\n'));

            //if (!Validator.ValidateTextBoxes(requiredTextBoxes))
            //    return;

            //CreatedBoss = temp;
            Back(sender, null);
        }
        //private void Create(object sender, RoutedEventArgs e)
        //{

        //}
        private void Load(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Boss temp = (Boss)FileHandler.LoadObject();

            //    if (temp == null)
            //        return;

            //    CreatedBoss = temp;

            //    SpecificName.Text = temp.SpecificName;
            //    GeneralName.Text = temp.GeneralName;
            //    Description.Text = temp.Description;

            //    BaseHP.Text = temp.BaseStats[0] + "";
            //    BaseMana.Text = temp.BaseStats[1] + "";
            //    BaseDef.Text = temp.BaseStats[2] + "";
            //    BaseDmg.Text = temp.BaseStats[3] + "";
            //    BaseSpd.Text = temp.BaseStats[4] + "";

            //    BossMessageStart.Text = temp.BossMessageStart.JoinArray("\r\n");
            //    BossMesageDefeat.Text = temp.BossMessageDefeat.JoinArray("\r\n");
            //    BossMessageVictory.Text = temp.BossMessageVictory.JoinArray("\r\n");
            //}
            //catch (Exception)
            //{
            //    return;
            //}
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
