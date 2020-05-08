using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Text
{
    public class Message
    {
        private System.Timers.Timer Timer = new System.Timers.Timer(100);

        private TextBlock TextBlock = new TextBlock();

        private int lineIndex;

        private int charIndex;

        public List<string> Lines { get; set; }

        public Message()
        {
            Lines = new List<string>();
        }

        /// <summary>
        /// Creates a new Text object that will make text available to diplay after conditions are met.
        /// </summary>
        /// <param name="lines">The message to display.</param>
        public Message(List<string> lines)
        {
            Lines = lines;
        }

        public void InitializeTextBlock(TextBlock textBlock)
        {
            TextBlock = textBlock;
            TextBlock.Text = "";
        }

        public bool IsDoneDisplaying()
        {
            try
            {
                _ = Lines[lineIndex];
                _ = Lines[lineIndex].ToCharArray()[charIndex];
            }
            catch (NullReferenceException)
            {
                return true;
            }
            catch (IndexOutOfRangeException)
            { return true; }

            return false;
        }

        public char GetNextChar()
        {
            charIndex++;
            return Lines[lineIndex].ToCharArray()[charIndex];
        }

        public void Display()
        {
            Timer.Elapsed += new ElapsedEventHandler(SetTextBlockText);
            Timer.Start();
        }

        private void SetTextBlockText(object sender, ElapsedEventArgs e)
        {
            if (TextBlock == null)
                throw new NotImplementedException("TextBlock needs to be implemented");

            App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    TextBlock.Text += GetNextChar();
                }
                catch (IndexOutOfRangeException)
                {
                    return;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            });
        }

        public void NextLine()
        {
            lineIndex++;
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

        /// <summary>
        /// Gets a preview of the message
        /// </summary>
        /// <param name="characters">The numbers of charecters before the ...</param>
        /// <returns></returns>
        public string GetPreview(int characters)
        {
            if (Lines == null || Lines.Count < 1)
                return "";
            string str = Lines[0];
            if (str == null)
                return "";
            else if (str.Count() <= characters)
                return str;
            else
            {
                str = str.Substring(0, characters) + "...";
                return str;
            }
        }
    }
}
