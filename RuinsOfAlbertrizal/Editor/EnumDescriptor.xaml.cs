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

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for EnumDescriptor.xaml
    /// </summary>
    public partial class EnumDescriptor : Window
    {
        public EnumDescriptor()
        {
            InitializeComponent();
        }

        public EnumDescriptor(Array enumValues)
        {
            InitializeComponent();

            for (int i = 0; i < enumValues.Length; i++)
            {
                Enum enumValue = (Enum)enumValues.GetValue(i);
                string tooltip = enumValue.GetDescription();

                TextBlock textBlock = new TextBlock
                {
                    Text = enumValue.ToString(),
                    ToolTip = tooltip,
                    FontSize = 28,
                    Margin = new Thickness(10)
                };

                InfoPanel.Children.Add(textBlock);
            }
        }
    }
}
