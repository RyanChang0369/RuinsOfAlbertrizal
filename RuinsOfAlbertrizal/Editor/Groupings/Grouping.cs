using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor.Groupings
{
    public abstract class Grouping
    {
        Label Label;
        Control Control;

        public enum ControlOptions
        {
            TextBox, TextBox_Paragraph, ComboBox
        }

        public Grouping(string header, int controlOption, string[] comboBoxItems)
        {
            Label = new Label();
            Label.Content = header;

            switch (controlOption)
            {
                case (int)ControlOptions.TextBox:
                    Control = new TextBox();
                    break;
                case (int)ControlOptions.TextBox_Paragraph:
                    Control = new TextBox();
                    Control.Style = Application.Current.FindResource("paragraphTextBox") as Style;
                    break;
                case (int)ControlOptions.ComboBox:
                    Control = new ComboBox();

                    break;
            }
        }
    }
}
