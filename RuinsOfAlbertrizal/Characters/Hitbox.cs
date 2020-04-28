using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal.Characters
{
    public class Hitbox
    {
        /// <summary>
        /// The width in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height in pixels
        /// </summary>
        public int Height { get; set; }

        public bool CanFit(Character character)
        {
            return (Width <= character.Hitbox.Width && Height <= character.Hitbox.Height);
        }

        public Hitbox()
        { }

        public Hitbox(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
