using RuinsOfAlbertrizal.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private string ObjectName = "";

        public MessageAdderPrompt()
        {
            InitializeComponent();
        }

        public MessageAdderPrompt(string objName, List<Message> messages)
        {
            InitializeComponent();
            ObjectName = objName;
            Header.Content = $"Add Message for {objName}";

            Messages = messages;

            foreach (Message message in messages)
            {
                AddedMessagesListBox.Items.Add(message.GetPreview(10));
            }
        }

        private void AddMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect(ObjectName, new Message());
            messageSelect.ShowDialog();
            AddedMessagesListBox.Items.Add(messageSelect.GetMessage().GetPreview(10));
            Messages.Add(messageSelect.GetMessage());
        }

        private void RemoveSelectionBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = AddedMessagesListBox.SelectedIndex;

            if (index < 0)
                return;

            AddedMessagesListBox.Items.RemoveAt(index);
            Messages.RemoveAt(index);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
