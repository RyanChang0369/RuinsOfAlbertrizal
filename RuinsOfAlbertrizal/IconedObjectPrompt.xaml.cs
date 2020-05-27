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

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for IconedObjectPrompt.xaml
    /// </summary>
    public partial class IconedObjectPrompt : Window
    {
        public IconedObjectPrompt()
        {
            InitializeComponent();
        }

        public IconedObjectPrompt(string title, string message, IconedObjectOfAlbertrizal obj, string yesBtnText, string noBtnText)
        {
            Title = title;
            MessageBlock.Text = message;
            IconImg.Source = obj.IconAsBitmapSource;
            ButtonYes.Content = yesBtnText;
            ButtonNo.Content = noBtnText;
        }

        private void ButtonYes_Click(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonNo_Click(object sender, EventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
        }
    }
}
