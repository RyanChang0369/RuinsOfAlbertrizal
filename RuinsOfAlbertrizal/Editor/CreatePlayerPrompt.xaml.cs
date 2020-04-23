using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreatePlayerPrompt.xaml
    /// </summary>
    public partial class CreatePlayerPrompt : EditorInterface
    {
        public CreatePlayerPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreateMapPrompt.Map.Player;
        }

        private void UpdateComponent()
        {
            if (CreateMapPrompt.Map.Player == null)
                CreateMapPrompt.Map.Player = new Player();
        }
    }
}
