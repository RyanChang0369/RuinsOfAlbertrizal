using RuinsOfAlbertrizal.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public abstract class CharacterMapBasedObject : WorldMapObject
    {
        [XmlIgnore]
        public Point ConnectionPoint => new Point(33, 19);

        [XmlIgnore]
        public Rectangle ConnectionArea => new Rectangle(33, 26, 4, 4);

        /// <summary>
        /// Displays armor. If using this, make sure to load/unload it.
        /// </summary>
        [XmlIgnore]
        public Bitmap ArmoredImage { get; set; }

        [XmlIgnore]
        public BitmapSource ArmoredImageAsBitmapSource => ArmoredImage.ToBitmapSource();

        public void LoadImage(Bitmap imageHelmet, Bitmap imageTorso, Bitmap imageLegs, Equiptment weapon)
        {
            int bx = -1, by = -1;
            if (weapon != null)
            {
                bx = ConnectionPoint.X - weapon.ConnectionPointX;
                by = weapon.ConnectionPointY - ConnectionPoint.Y;
                int calculatedX = bx + 48;
                int calculatedY = by + 48;
                ArmoredImage = new Bitmap(calculatedX, calculatedY);
            }
            else
            {
                ArmoredImage = new Bitmap(48, 48);
            }

            using (Graphics g = Graphics.FromImage(ArmoredImage))
            {
                g.DrawImage(WorldImg, 0, by, 48, 48);

                if (imageHelmet != null)
                {
                    g.DrawImage(imageHelmet, 0, 34);
                }

                if (imageTorso != null)
                {
                    g.DrawImage(imageTorso, 0, 18);
                }

                if (imageLegs != null)
                {
                    g.DrawImage(imageLegs, 0, 0);
                }

                if (weapon != null)
                {
                    g.DrawImage(weapon.Icon, bx, 0, 48, 48);
                }
            }
        }

        public void UnloadImage()
        {
            ArmoredImage = null;
        }
    }
}
