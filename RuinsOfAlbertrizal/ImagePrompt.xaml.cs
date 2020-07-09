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
    /// Interaction logic for ImagePrompt.xaml
    /// </summary>
    public partial class ImagePrompt : Window
    {
        public ImagePrompt()
        {
            InitializeComponent();
        }

        public ImagePrompt(string title, string message, BitmapSource bitmapSource)
        {
            Title = title;
            MessageBlock.Text = message;
            IconImg.Source = bitmapSource;
            ButtonCancel.Content = "OK";
            ButtonYes.Visibility = Visibility.Collapsed;
            ButtonNo.Visibility = Visibility.Collapsed;
        }

        public ImagePrompt(string title, string message, BitmapSource bitmapSource, string cancelText)
        {
            Title = title;
            MessageBlock.Text = message;
            IconImg.Source = bitmapSource;
            ButtonCancel.Content = cancelText;
            ButtonYes.Visibility = Visibility.Collapsed;
            ButtonNo.Visibility = Visibility.Collapsed;
        }

        public ImagePrompt(string title, string message, BitmapSource bitmapSource, string yesBtnText, string noBtnText)
        {
            Title = title;
            MessageBlock.Text = message;
            IconImg.Source = bitmapSource;
            ButtonYes.Content = yesBtnText;
            ButtonNo.Content = noBtnText;
            ButtonCancel.Visibility = Visibility.Collapsed;
        }

        public ImagePrompt(string title, string message, BitmapSource bitmapSource, string yesText, string noText, string cancelText)
        {
            Title = title;
            MessageBlock.Text = message;
            IconImg.Source = bitmapSource;
            ButtonYes.Content = yesText;
            ButtonNo.Content = noText;
            ButtonCancel.Content = cancelText;
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
            DialogResult = null;
        }
    }
}
