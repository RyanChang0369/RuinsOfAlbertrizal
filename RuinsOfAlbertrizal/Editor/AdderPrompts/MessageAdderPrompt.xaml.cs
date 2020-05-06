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
    /// Interaction logic for MessageAdderPrompt.xaml
    /// </summary>
    public partial class MessageAdderPrompt : Window
    {
        public List<Message> Messages = new List<Message>();

        private List<string> Lines = new List<string>();

        private string ObjectName = "";

        public MessageAdderPrompt()
        {
            InitializeComponent();
        }

        public MessageAdderPrompt(string objName)
        {
            ObjectName = objName;
            Header.Content = $"Add Message for {objName}";
        }

        private void AddMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect(ObjectName, new Message());
            messageSelect.ShowDialog();
            AddedMessagesListBox.Items.Add(messageSelect.GetLines()[0]);
        }

        private void AddedMessagesListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = AddedMessagesListBox.SelectedIndex;

            AddedMessagesListBox.Items.RemoveAt(index);
            Messages.RemoveAt(index);
        }
    }
}
