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
        public static Player CreatedPlayer { get; set; }

        public CreatePlayerPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedPlayer;
        }

        protected override void UpdateComponent()
        {
            if (CreatedPlayer == null)
                CreatedPlayer = new Player();
        }

        public override void ClearVariable()
        {
            CreatedPlayer = new Player();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(PlayerName);
            RequiredControls.Add(Class);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedPlayer.IconLocation = FileHandler.SaveBitmap(CreatedPlayer, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedPlayer.WorldImgLocation = FileHandler.SaveBitmap(CreatedPlayer, "worldImg");
            }
            catch (ArgumentException)
            {

            }
        }
    }
}
