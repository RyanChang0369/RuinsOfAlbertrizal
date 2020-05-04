using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Has both an icon and a in-game image
    /// </summary>
    public class WorldMapObject : IconedObjectOfAlbertrizal
    {
        public string WorldImgLocation { get; set; }

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
    }
}
