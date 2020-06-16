using RuinsOfAlbertrizal.Editor;
using RuinsOfAlbertrizal.Mechanics;
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
    /// Interaction logic for BuffDisplayer.xaml
    /// </summary>
    public partial class BuffDisplayer : BaseWindow
    {
        public BuffDisplayer(List<Buff> buffs)
        {
            InitializeComponent();
            DataContext = buffs;
        }
        private void InfoLoaded_EnumDescription(object sender, RoutedEventArgs e)
        {
            Image imgElement = (Image)sender;
            imgElement.ToolTip = "Click to open description for the below Combo Box.";

            imgElement.MouseUp += InfoMouseUp_EnumDescription;
        }

        private void InfoMouseUp_EnumDescription(object sender, MouseEventArgs e)
        {
            Image imgElement = (Image)sender;

            if (e.LeftButton == MouseButtonState.Released)
            {
                EnumDescriptor descriptor = new EnumDescriptor((Array)imgElement.Tag);
                descriptor.Show();
            }
        }
    }
}
