using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    public abstract class IconedObjectOfAlbertrizal : ObjectOfAlbertrizal
    {
        public string IconLocation { get; set; }

        protected Bitmap icon = Properties.Resources.error;

        [XmlIgnore]
        public Bitmap Icon
        {
            get
            {
                try
                {
                    icon = new Bitmap(IconLocation);
                }
                catch (Exception)
                {

                }
                return icon;
            }
        }
    }
}
