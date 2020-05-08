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
        private System.Timers.Timer TimerChar = new System.Timers.Timer(100);

        private System.Timers.Timer TimerLine = new System.Timers.Timer(2000);

        private TextBlock TextBlock = new TextBlock();

        private Button NextBtn = new Button();

        private Button SkipBtn = new Button();

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

        public void InitializeControls(TextBlock textBlock, Button nextButton, Button skipButton)
        {
            TextBlock = textBlock;
            TextBlock.Text = "";
            NextBtn = nextButton;
            NextBtn.Click += new RoutedEventHandler(NextBtn_Click); 
            SkipBtn = skipButton;
            SkipBtn.Click += new RoutedEventHandler(SkipBtn_Click);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            TimerChar.Stop();
            NextLine();
            Display();
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            TimerChar.Stop();
        }

        private bool IsDoneDisplaying()
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
            catch (ArgumentOutOfRangeException)
            { return true; }

            return false;
        }

        public char GetNextChar()
        {
            char c = Lines[lineIndex].ToCharArray()[charIndex];
            charIndex++;
            return c;
        }

        public void Display()
        {
            if (IsDoneDisplaying())
                return;

            TimerChar.Elapsed += new ElapsedEventHandler(SetTextBlockText);

            TimerChar.Start();
        }

        private void SetTextBlockText(object sender, ElapsedEventArgs e)
        {
            if (TextBlock == null)
                throw new ArgumentException("TextBlock needs to be implemented");

            try
            {
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
            catch (NullReferenceException)
            {
                return;
            }
        }

        public void NextLine()
        {
            charIndex = 0;
            lineIndex++;
            TextBlock.Text = "";
            TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
        }

        public void NextLine(object sender, ElapsedEventArgs e)
        {
            NextLine();
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
