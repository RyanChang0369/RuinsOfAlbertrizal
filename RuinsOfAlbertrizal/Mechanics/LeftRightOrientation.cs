using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Mechanics
{
    /// <summary>
    /// Which way the character is facing
    /// </summary>
    public class LeftRightOrientation
    {
        public enum Direction
        {
            Left, Right
        }

        public Direction Orientation { get; set; }

        public LeftRightOrientation()
        {
            Orientation = Direction.Left;
        }

        public LeftRightOrientation(Direction orientation)
        {
            Orientation = orientation;
        }
    }
}
