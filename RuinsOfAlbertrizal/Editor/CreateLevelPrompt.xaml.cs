using System;
using System.Linq;
using System.Windows;
using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateLevelPrompt.xaml
    /// </summary>
    public partial class CreateLevelPrompt : EditorInterface
    {
        public static Level CreatedLevel { get; set; }

        public CreateLevelPrompt() : base()
        {
            InitializeComponent();
            DataContext = CreatedLevel;
        }

        public CreateLevelPrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedLevel;
        }

        protected override void UpdateComponent()
        {
            if (CreatedLevel == null)
                CreatedLevel = new Level();
        }

        public override void ClearVariable()
        {
            CreatedLevel = new Level();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(LevelName);
            RequiredControls.Add(Difficulty);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedLevel.IconLocation = FileHandler.SaveBitmap(CreatedLevel, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedLevel.WorldImgLocation = FileHandler.SaveBitmap(CreatedLevel, "worldImg");
            }
            catch (ArgumentException)
            {

            }
        }

        private void AddBossBtn_Click(object sender, RoutedEventArgs e)
        {
            SimpleAdderPrompt prompt = new SimpleAdderPrompt(CreatedLevel.Bosses.Cast<ObjectOfAlbertrizal>().ToList(),
                Map.StoredBosses.Cast<ObjectOfAlbertrizal>().ToList(), "Add/Remove Bosses");
            prompt.ShowDialog();
            CreatedLevel.BossGuids = prompt.GetSelected<Boss>().ToGlobalIDList();
            CreatedLevel.RefreshStoredObjects();
        }

        private void AddEnemyBtn_Click(object sender, RoutedEventArgs e)
        {
            SimpleAdderPrompt simpleAdderPrompt = new SimpleAdderPrompt(CreatedLevel.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), Map.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), "Add/Remove Enemies");
            simpleAdderPrompt.ShowDialog();
            CreatedLevel.StoredEnemyGuids = simpleAdderPrompt.GetSelected<Enemy>().ToGlobalIDList();
            CreatedLevel.RefreshStoredObjects();
        }

        private void AddIntroMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect("Introduction Message", CreatedLevel.IntroMessage);
            messageSelect.ShowDialog();
            CreatedLevel.IntroMessage = messageSelect.GetMessage();
        }
    }
}
