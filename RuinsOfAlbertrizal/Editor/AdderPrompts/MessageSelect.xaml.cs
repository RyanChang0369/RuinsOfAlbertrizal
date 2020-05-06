using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    /// <summary>
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class MessageSelect : Window
    {
        public MessageSelect()
        {
            InitializeComponent();
        }

        public MessageSelect(string objectName, Message message)
        {
            InitializeComponent();
            this.Title = $"Enter a Message for {objectName}";

            try
            {
                ValueTextBox.Text = message.ToString();
            }
            catch (NullReferenceException)
            {  }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public List<string> GetLines()
        {
            string[] delimiter = { "\r\n" };

            try
            {
                return ((string)ValueTextBox.Text).Split(delimiter, StringSplitOptions.None).ToList<string>();
            }
            catch (NullReferenceException)
            {
                List<string> emptyList = new List<string>();
                emptyList.Add("");
                return emptyList;
            }
        }

        public Message GetMessage()
        {
            return new Message(GetLines());
        }
    }
}
