using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Mechanics
{
    public class CardinalOrientation
    {
        public enum Direction
        {
            North, East, South, West
        }

        public Direction Orientation { get; set; }

        public CardinalOrientation()
        {
            Orientation = Direction.North;
        }

        public CardinalOrientation(Direction orientation)
        {
            Orientation = orientation;
        }
    }
}
