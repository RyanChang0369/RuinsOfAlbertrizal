﻿using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public class GameLoader
    {
        public static void NewCampaign()
        {
            GameBase.NewGame(FileHandler.LoadMap("UserCampaign/newMap.xml"));
        }

        public static void LoadCampaign()
        {
            GameBase.NewGame(FileHandler.LoadMap("UserCampaign/userMap.xml"));
        }

        /// <summary>
        /// Opens a file dialog to load a custom map.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LoadCustomMap()
        {
            FileDialog dialog = new FileDialog((int)FileDialog.DialogOptions.Load);

            try
            {
                GameBase.NewGame(FileHandler.LoadMap(dialog.GetPath()));
            }
            catch (ArgumentNullException)
            { return; }
        }
    }
}
