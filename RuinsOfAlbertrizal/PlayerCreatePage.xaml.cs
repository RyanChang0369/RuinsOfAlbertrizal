﻿using RuinsOfAlbertrizal.Editor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Interaction logic for PlayerCreatePage.xaml
    /// </summary>
    public partial class PlayerCreatePage : BasePage
    {
        public PlayerCreatePage()
        {
            InitializeComponent();

            DataContext = GameBase.CurrentGame;

            if (CreatePlayerPrompt.CreatedPlayer != null)
            {
                GameBase.CurrentGame.Players.Add(CreatePlayerPrompt.CreatedPlayer);
            }
        }
    }
}
