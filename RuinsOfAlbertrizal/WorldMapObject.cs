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
    /// <summary>
    /// Has both an icon and a in-game image
    /// </summary>
    
    public abstract class WorldMapObject : IconedObjectOfAlbertrizal, INotifyPropertyChanged
    {
        private string worldImgLocation;

        public string WorldImgLocation
        {
            get => worldImgLocation;
            set
            {
                worldImgLocation = value;
                OnPropertyChanged();
            }
        }

        protected Bitmap worldImg = Properties.Resources.error;
        

        [XmlIgnore]
        public Bitmap WorldImg
        {
            get
            {
                try
                {
                    worldImg = new Bitmap(Path.Combine(GameBase.CurrentMapLocation, worldImgLocation));
                }
                catch (Exception)
                {

                }
                return worldImg;
            }
        }

        [XmlIgnore]
        public BitmapSource WorldImgAsBitmapSource
        {
            get => WorldImg.ToBitmapSource();
        }

        public WorldMapObject() : base()
        {

        }

        [XmlIgnore]
        public bool WorldImgIsValid
        {
            get
            {
                return File.Exists(Path.Combine(GameBase.CurrentMapLocation, worldImgLocation));
            }
        }
    }
}
