using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor.Converter
{
    public class ArrayConverter : IValueConverter
    {
        /// <summary>
        /// Message to string array
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
                return ArrayMethods.JoinArray((string[])value, "\r\n");
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        /// <summary>
        /// String array to message
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
