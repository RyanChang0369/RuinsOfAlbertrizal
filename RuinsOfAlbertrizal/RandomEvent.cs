using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public class RandomEvent : IComparable<RandomEvent>
    {
        /// <summary>
        /// The larger the chance, the more likely the event will happen.
        /// Chances range from 0.0 - 1.0. Chances with values of 1.0 are NOT GUAREENTEED to happen,
        /// but chances with values of 0.0 are GUAREENTEED NOT to happen.
        /// </summary>
        public double Chance { get; set; }

        public object Tag { get; set; }

        /// <summary>
        /// Creates a new random event.
        /// </summary>
        /// <param name="tag">The name of the event.</param>
        /// <param name="chance">The larger the chance, the more likely the event will happen.
        /// Chances range from 0.0 - 1.0. Chances with values of 1.0 are NOT GUAREENTEED to happen,
        /// but chances with values of 0.0 are GUAREENTEED NOT to happen.</param>
        public RandomEvent(object tag, double chance)
        {
            Tag = tag;
            Chance = chance;
        }

        public int CompareTo(RandomEvent other)
        {
            return Chance.CompareTo(other.Chance);
        }
    }
}
