﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor.Converter
{
    public class NameConverter : IValueConverter
    {
        /// <summary>
        /// ObjectsOfAlbertrizal to string[]
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
                List<ObjectOfAlbertrizal> objectsOfAlbertrizal = (value as IEnumerable<ObjectOfAlbertrizal>).Cast<ObjectOfAlbertrizal>().ToList();

                return ObjectOfAlbertrizal.GetNames(objectsOfAlbertrizal);
            }
            catch (ArgumentNullException)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This type of conversion is not supported");
        }
    }
}
