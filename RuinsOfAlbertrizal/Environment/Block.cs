using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Block : ObjectOfAlbertrizal, INotifyPropertyChanged
    {
        private string tileImageLocation;

        public string TileImageLocation
        {
            get => tileImageLocation;
            set
            {
                tileImageLocation = value;
                OnPropertyChanged();
            }
        }

        protected Bitmap tileImage = Properties.Resources.error;

        [XmlIgnore]
        public Bitmap TileImage
        {
            get
            {
                try
                {
                    tileImage = new Bitmap(Path.Combine(GameBase.CurrentMapLocation, tileImageLocation));
                }
                catch (Exception)
                {

                }
                return tileImage;
            }
        }

        public System.Windows.Point Location { get; set; }

        public enum BlockType
        {
            /// <summary>
            /// Characters appear behind this hazard and can stand on it.
            /// </summary>
            [Display(Name = "Tangable Block", Description = "Characters appear behind this hazard and can stand on it.")]
            [Description("Characters appear behind this hazard and can stand on it.")]
            TangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and can stand on it.
            /// </summary>
            [Display(Name = "Tangable Wall", Description = "Characters appear ahead of this hazard and can stand on it.")]
            [Description("Characters appear ahead of this hazard and can stand on it.")]
            TangableWall,
            /// <summary>
            /// Characters appear behind this hazard and cannot stand on it.
            /// </summary>
            [Display(Name = "Intangable Block", Description = "Characters appear behind this hazard and cannot stand on it.")]
            [Description("Characters appear behind this hazard and cannot stand on it.")]
            IntangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and cannot stand on it.
            /// </summary>
            [Display(Name = "Intangable Wall", Description = "Characters appear ahead of this hazard and cannot stand on it.")]
            [Description("Characters appear ahead of this hazard and cannot stand on it.")]
            IntangableWall
        }

        public BlockType TypeOfBlock { get; set; }

        public Block()
        {
            TypeOfBlock = new BlockType();
        }
    }
}
