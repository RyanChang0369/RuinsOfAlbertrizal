using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for IntroInterface.xaml
    /// </summary>
    public partial class IntroInterface : Page
    {
        public IntroInterface()
        {
            InitializeComponent();
            WriteText();
        }
        private void WriteText()
        {
            List<string> text = GameBase.CurrentGame.IntroText;
            foreach (string line in text)
            {
                IntroText.Text = "";
                char[] charecters = line.ToCharArray();
                foreach (char charecter in charecters)
                {
                    IntroText.Text += charecter;
                    Thread.Sleep(11);
                }
                Thread.Sleep(5555);
            }

            NavAdventureInterface();
        }

        private void NavAdventureInterface()
        {
            this.NavigationService.Navigate(new Uri("AdventureInterface.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
