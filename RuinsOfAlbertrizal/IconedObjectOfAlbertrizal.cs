using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public abstract class IconedObjectOfAlbertrizal : ObjectOfAlbertrizal, INotifyPropertyChanged
    {
        private string iconLocation;
        
        public string IconLocation 
        {
            get => iconLocation;
            set
            {
                iconLocation = value;
                OnPropertyChanged();
            }
        }

        protected Bitmap icon = Properties.Resources.error;

        [XmlIgnore]
        public Bitmap Icon
        {
            get
            {
                try
                {
                    icon = new Bitmap(Path.Combine(GameBase.CurrentMapLocation, iconLocation));
                }
                catch (Exception)
                {

                }
                return icon;
            }
        }

        [XmlIgnore]
        public BitmapSource IconAsBitmapSource
        {
            get => Icon.ToBitmapSource();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string iconLocation = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(iconLocation));
        }
    }
}
