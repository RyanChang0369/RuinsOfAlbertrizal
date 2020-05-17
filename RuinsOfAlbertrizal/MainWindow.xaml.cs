using RuinsOfAlbertrizal.Editor;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CreateMapPrompt.Map == null)
            {
                return;
            }
            //else if (!GameBase.Initialized() || !CreateMapPrompt.Map.Equals(GameBase.CurrentGame))
            //{
            //    MessageBoxResult result = MessageBox.Show("You have unsaved data. Press OK to discard all changes.", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            //    if (result == MessageBoxResult.Cancel)
            //        e.Cancel = true;
            //}
        }
    }
}
