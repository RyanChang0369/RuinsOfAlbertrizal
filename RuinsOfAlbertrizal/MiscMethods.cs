using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public static class MiscMethods
    {
        private static readonly Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        public static bool IsValid(this DependencyObject control)
        {
            if (Validation.GetHasError(control))
                return false;
            else
                return true;
        }

        public static BitmapSource ToBitmapSource(this System.Drawing.Bitmap bitmap)
        {
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
            catch (NullReferenceException)
            {
                return new BitmapImage();
            }
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return "";
        }

        public static string GetSeperator(int i, int max)
        {
            string seperator = "";
            if (i > 0)
            {
                if (max - i == 1)
                {
                    seperator = ", and";
                }
                else
                    seperator = ",";
            }

            return seperator;
        }

        public async static Task TaskDelay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        /// <summary>
        /// Clones an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thing">Any Xml serializable object</param>
        /// <returns></returns>
        public static T MemoryClone<T>(this T thing)
        {
            XmlSerializer serializer = new XmlSerializer(thing.GetType());
            MemoryStream memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, thing);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(memoryStream);
        }
    }
}
