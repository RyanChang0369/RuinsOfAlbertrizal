using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor
{
    public class ArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ArrayMethods.JoinArray((string[])value, "\r\n");
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] delimiter = { "\r\n" };

            try
            {
                return ((string)value).Split(delimiter, StringSplitOptions.None);
            }
            catch (NullReferenceException)
            {
                string[] emptyArr = { "" };
                return emptyArr;
            }
        }
    }
}
