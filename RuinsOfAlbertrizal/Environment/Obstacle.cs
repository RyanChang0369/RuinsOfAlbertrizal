using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Environment
{
    public class Obstacle
    {
        public string Name { get; set; }

        public int PathOfTravel { get; set; }
        /// <summary>
        /// Specifies how characters can travel through this obstacle.
        /// </summary>
        public enum PathsOfTravel
        {
            All,
            UpAndDown,
            LeftAndRight,
            UpAndLeft,
            UpAndRight,
            DownAndLeft,
            DownAndRight
        }

        /// <summary>
        /// The maximum sized character than can pass through this obstacle
        /// </summary>
        public int MaximumSize { get; set; }

        public Obstacle(string name, int pathOfTravel, int maximumSize)
        {
            PathOfTravel = pathOfTravel;
            Name = name;
            MaximumSize = maximumSize;
        }
    }
}
