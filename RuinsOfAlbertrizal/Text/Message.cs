using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Text
{
    public class Message
    {
        public string Text { get; set; }

        /// <summary>
        /// What point completion should this fire at?
        /// </summary>
        public double PointCompletion { get; set; }

        /// <summary>
        /// Creates a new Text object that will make text available to diplay after conditions are met.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="pointCompletion">The point completion when the message will be fired at.</param>
        public Message(string text, double pointCompletion)
        {
            Text = text;
            PointCompletion = pointCompletion;
        }

        [XmlIgnore]
        public bool ReadyToDisplay { get => PointCompletion >= GameBase.CurrentGame.CurrentLevel.Points; }

        public void Display()
        {
            throw new NotImplementedException("Add in display method from GameOfNo");
        }
    }
}
