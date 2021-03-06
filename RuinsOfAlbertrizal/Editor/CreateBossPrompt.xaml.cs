﻿using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RuinsOfAlbertrizal.Editor
{
    /// <summary>
    /// Interaction logic for CreateBossPrompt.xaml
    /// </summary>
    public partial class CreateBossPrompt : EditorInterface
    {
        public static Boss CreatedBoss { get; set; }

        public CreateBossPrompt()
        {
            InitializeComponent();
            DataContext = CreatedBoss;
        }

        public CreateBossPrompt(Map map) : base(map)
        {
            InitializeComponent();
            DataContext = CreatedBoss;
        }

        protected override void UpdateComponent()
        {
            if (CreatedBoss == null)
                CreatedBoss = new Boss();
        }

        public override void ClearVariable()
        {
            CreatedBoss = new Boss();
        }

        protected override void AddRequiredControls()
        {
            RequiredControls.Add(BossName);
            RequiredControls.Add(BaseDef);
            RequiredControls.Add(BaseDmg);
            RequiredControls.Add(BaseHP);
            RequiredControls.Add(BaseMana);
            RequiredControls.Add(BaseInt);
            RequiredControls.Add(BaseSpd);
        }

        private void SelectIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedBoss.IconLocation = FileHandler.SaveBitmap(CreatedBoss, "icon");
            }
            catch (ArgumentException)
            {

            }
        }

        private void SelectWorldImgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreatedBoss.WorldImgLocation = FileHandler.SaveBitmap(CreatedBoss, "worldImg");
            }
            catch (ArgumentException)
            {

            }
        }

        private void AddEncounterMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageAdderPrompt messageAdderPrompt = new MessageAdderPrompt("Boss Encounter", CreatedBoss.MessagesOnEncounter);
            messageAdderPrompt.ShowDialog();
            CreatedBoss.MessagesOnEncounter = messageAdderPrompt.Messages;
        }

        private void AddAttackMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageAdderPrompt messageAdderPrompt = new MessageAdderPrompt("Boss Attack", CreatedBoss.MessagesOnAttack);
            messageAdderPrompt.ShowDialog();
            CreatedBoss.MessagesOnAttack = messageAdderPrompt.Messages;
        }

        private void AddDefeatMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageAdderPrompt messageAdderPrompt = new MessageAdderPrompt("Boss Defeat", CreatedBoss.MessagesOnDefeat);
            messageAdderPrompt.ShowDialog();
            CreatedBoss.MessagesOnDefeat = messageAdderPrompt.Messages;
        }

        private void AddVictoryMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageAdderPrompt messageAdderPrompt = new MessageAdderPrompt("Boss Victory", CreatedBoss.MessagesOnVictory);
            messageAdderPrompt.ShowDialog();
            CreatedBoss.MessagesOnVictory = messageAdderPrompt.Messages;
        }
    }
}
