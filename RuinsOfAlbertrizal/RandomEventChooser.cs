using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public class RandomEventChooser
    {
        /// <summary>
        /// A list of chances with larger numbers being more likely to occur.
        /// </summary>
        public List<RandomEvent> RandomEvents { get; set; }

        public RandomEventChooser(List<RandomEvent> randomEvents)
        {
            RandomEvents = randomEvents;
        }

        /// <summary>
        /// Gets the selected random event
        /// </summary>
        /// <returns></returns>
        public RandomEvent GetSelectedRandomEvent()
        {
            List<RandomEvent> selectedEvents = new List<RandomEvent>();

            foreach (RandomEvent randomEvent in RandomEvents)
            {
                double fateSelector = RNG.GetRandomDouble();
                if (randomEvent.Chance > fateSelector)
                    selectedEvents.Add(randomEvent);
            }

            if (selectedEvents.Count < 1)
                return GetSelectedRandomEvent();
            else
            {
                int fateSelector2 = RNG.GetRandomInteger(selectedEvents.Count);
                return selectedEvents[fateSelector2];
            }
        }

        public object GetSelectedTag()
        {
            return GetSelectedRandomEvent().Tag;
        }
    }
}
