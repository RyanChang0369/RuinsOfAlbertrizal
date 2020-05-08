﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public CreateLevelPrompt()
        {
            InitializeComponent();
            UpdateComponent();
            DataContext = CreatedLevel;
        }

        protected override void UpdateComponent()
        {
            if (CreatedLevel == null)
                CreatedLevel = new Level();
        }

        protected override void ClearVariable()
        {
            CreatedLevel = new Level();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(LevelName);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedLevel.IconLocation = FileHandler.SaveBitmap(CreatedLevel, "icon");
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            CreatedLevel.WorldImgLocation = FileHandler.SaveBitmap(CreatedLevel, "worldImg");
        }

        private void AddBossBtn_Click(object sender, RoutedEventArgs e)
        {
            BossAdderPrompt bossAdderPrompt = new BossAdderPrompt(CreatedLevel.Bosses);
            bossAdderPrompt.ShowDialog();
            CreatedLevel.Bosses = bossAdderPrompt.TargetBosses;
        }

        private void AddEnemyBtn_Click(object sender, RoutedEventArgs e)
        {
            SimpleAdderPrompt simpleAdderPrompt = new SimpleAdderPrompt(CreatedLevel.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), CreateMapPrompt.Map.StoredEnemies.Cast<ObjectOfAlbertrizal>().ToList(), "Add/Remove Enemies");
            simpleAdderPrompt.ShowDialog();
            CreatedLevel.Bosses = simpleAdderPrompt.StoredObjects.Cast<Boss>().ToList();
        }

        private void AddIntroMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageSelect messageSelect = new MessageSelect("Introduction Message", CreatedLevel.IntroMessage);
            messageSelect.ShowDialog();
            CreatedLevel.IntroMessage = messageSelect.GetMessage();
        }
    }
}
