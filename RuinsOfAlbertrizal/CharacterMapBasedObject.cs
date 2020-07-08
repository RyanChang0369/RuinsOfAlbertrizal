using RuinsOfAlbertrizal.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public abstract class CharacterMapBasedObject : WorldMapObject
    {
        [XmlIgnore]
        public Point ConnectionPoint => new Point(33, 26);

        [XmlIgnore]
        public Rectangle ConnectionArea => new Rectangle(33, 26, 4, 4);

        /// <summary>
        /// Displays armor. If using this, make sure to load/unload it.
        /// </summary>
        [XmlIgnore]
        public Bitmap ArmoredImage { get; set; }

        public void LoadImage(Bitmap imageHelmet, Bitmap imageTorso, Bitmap imageLegs, Equiptment weapon)
        {
            if (weapon != null)
            {
                int calculatedX = Math.Max(48, 48 + ConnectionPoint.X - weapon.ConnectionPoint.X);
                int calculatedY = Math.Max(48, 48 + ConnectionPoint.Y - weapon.ConnectionPoint.Y);
                ArmoredImage = new Bitmap(calculatedX, calculatedY);
            }
            else
            {
                ArmoredImage = new Bitmap(48, 48);
            }

            using (Graphics g = Graphics.FromImage(ArmoredImage))
            {
                g.DrawImage(WorldImg, 0, 0);

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
                    g.DrawImage(weapon.Icon, ConnectionPoint.X - weapon.ConnectionPoint.X, ConnectionPoint.Y - weapon.ConnectionPoint.Y);
                }
            }
        }

        public void UnloadImage()
        {
            ArmoredImage = null;
        }
    }
}
