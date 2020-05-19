using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor.Converter
{
    public class SelectedItemConverter : IValueConverter
    {
        /// <summary>
        /// SelectedItem to enum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)value;
                return (Enum)comboBoxItem.Content;
            }
            catch (InvalidCastException)
            {
                //Test
                return (Enum)value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
