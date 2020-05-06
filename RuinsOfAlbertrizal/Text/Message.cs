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
        public List<string> Lines { get; set; }

        public Message()
        { }

        /// <summary>
        /// Creates a new Text object that will make text available to diplay after conditions are met.
        /// </summary>
        /// <param name="lines">The message to display.</param>
        /// <param name="pointCompletion">The point completion when the message will be fired at.</param>
        public Message(List<string> lines)
        {
            Lines = lines;
        }

        public void Display()
        {
            throw new NotImplementedException("Add display method in BaseGame");
        }

        public override string ToString()
        {
            return ToString("\r\n");
        }

        public string ToString(string delimiter)
        {
            try
            {
                return ArrayMethods.JoinList(Lines, delimiter);
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        public string[] ToStringArray()
        {
            return Lines.ToArray();
        }

        public List<string> ToStringList()
        {
            return Lines;
        }
    }
}
