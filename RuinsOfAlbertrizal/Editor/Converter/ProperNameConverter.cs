using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor.Converter
{
    public class ProperNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((ObjectOfAlbertrizal)value).DisplayName;
            }
            catch (InvalidCastException)
            {
                return ((List<ObjectOfAlbertrizal>)value)[((List<ObjectOfAlbertrizal>)value).Count - 1].DisplayName;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
