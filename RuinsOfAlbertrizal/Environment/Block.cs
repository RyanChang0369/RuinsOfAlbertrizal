using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Block : ObjectOfAlbertrizal
    {
        public Bitmap TileImage { get; set; }

        public Point Location { get; set; }

        public enum BlockType
        {
            /// <summary>
            /// Characters appear behind this hazard and can stand on it. 
            /// </summary>
            [Description("Characters appear behind this hazard and can stand on it.")]
            TangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and can stand on it.
            /// </summary>
            [Description("Characters appear ahead of this hazard and can stand on it.")]
            TangableWall,
            /// <summary>
            /// Characters appear behind this hazard and cannot stand on it. 
            /// </summary>
            [Description("Characters appear behind this hazard and cannot stand on it.")]
            IntangableBlock,
            /// <summary>
            /// Characters appear ahead of this hazard and cannot stand on it.
            /// </summary>
            [Description("Characters appear ahead of this hazard and cannot stand on it.")]
            IntangableWall
        }
    }
}
