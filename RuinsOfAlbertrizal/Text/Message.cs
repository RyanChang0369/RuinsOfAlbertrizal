using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Text
{
    
    public class Message
    {
        private Timer TimerChar = new Timer(50);

        private TextBlock TextBlock = new TextBlock();

        private Button NextBtn = new Button();

        private Button SkipBtn = new Button();

        private int lineIndex;

        private int charIndex;

        public List<string> Lines { get; set; }

        public string FormattedLines
        {
            get
            {
                string lines = "";

                foreach (string str in Lines)
                    lines = $"{lines}\r\n{str}";

                return lines;
            }
        }

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

        public bool IsEmpty()
        {
            return Lines.Count == 0;
        }

        public void Add(string line)
        {
            Lines.Add(line);
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

        ///// <summary>
        ///// Initializes a TextBlock so that it automatically updates when Lines get updated.
        ///// </summary>
        ///// <param name="textBlock"></param>
        //public void IntializeControls(TextBlock textBlock)
        //{
        //    TextBlock = textBlock;
        //    TextBlock.Text = "";
        //}

        //private void LinesChanged()
        //{
        //    if (TextBlock == null)
        //        return;

        //    try
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            try
        //            {
        //                TextBlock.Text += GetNextChar();
        //            }
        //            catch (IndexOutOfRangeException)
        //            {
        //                TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
        //                return;
        //            }
        //            catch (ArgumentOutOfRangeException)
        //            {
        //                TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
        //                return;
        //            }
        //            catch (TaskCanceledException)
        //            {
        //                TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
        //                return;
        //            }
        //        });
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return;
        //    }
        //}

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsDoneDisplaying(lineIndex + 1))
            {
                SkipBtn_Click(sender, e);
                return;
            }

            NextLine();
            Display();
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            TimerChar.Stop();
        }

        /// <summary>
        /// Returns true when all messages are displayed
        /// </summary>
        /// <returns></returns>
        public bool NextBtnIsNavigate()
        {
            return IsDoneDisplaying(lineIndex + 1);
        }

        private bool IsDoneDisplaying(int index)
        {
            try
            {
                _ = Lines[index];
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
            if (IsDoneDisplaying(lineIndex))
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
                        TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
                        return;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
                        return;
                    }
                    catch (TaskCanceledException)
                    {
                        TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
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
            TimerChar.Stop();
            charIndex = 0;
            lineIndex++;
            TextBlock.Text = "";
            TimerChar.Elapsed -= new ElapsedEventHandler(SetTextBlockText);
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

        [XmlIgnore]
        public string GetPreviewAsProperty
        {
            get
            {
                return GetPreview(40);
            }
        }

        public void Reset()
        {
            TimerChar = new System.Timers.Timer(100);
            lineIndex = 0;
            charIndex = 0;
            TextBlock = new TextBlock();
            NextBtn = new Button();
            SkipBtn = new Button();
        }
    }
}
