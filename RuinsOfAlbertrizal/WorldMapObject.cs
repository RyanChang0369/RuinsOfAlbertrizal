using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Has both an icon and a in-game image
    /// </summary>
    public class WorldMapObject : IconedObjectOfAlbertrizal, INotifyPropertyChanged
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
                    worldImg = new Bitmap(WorldImgLocation);
                }
                catch (Exception)
                {

                }
                return worldImg;
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public new void OnPropertyChanged([CallerMemberName] string worldImgLocation = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(worldImgLocation));
        }
    }
}
