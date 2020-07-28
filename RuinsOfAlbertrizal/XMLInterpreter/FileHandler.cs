using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.XMLInterpreter
{
    public static class FileHandler
    {
        /// <summary>
        /// Reads all lines of the file without causing a lock.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <param name="fileName">The file to read.</param>
        /// <returns>The contents of the file.</returns>
        public static string SilentlyReadAllLines(string fileName)
        {
            using (FileStream fs = File.Open(fileName,
                        FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Reads all lines of a file whether or not it is in use.
        /// </summary>
        /// <param name="fileName">The file to read.</param>
        /// <returns>The contents of the file.</returns>
        public static string ForceReadAllLines(string fileName)
        {
            while (true)
            {
                try
                {
                    return SilentlyReadAllLines(fileName);
                }
                catch (Exception)
                {
                    Thread.Sleep(25);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CreateCustomCampaign()
        {
            FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Save, "XAML File|.xml", "map-static");

            GameBase.NewCustomCampaign(dialog.GetPath());
        }


        /// <summary>
        /// Opens a file dialog to load a custom map (loads current map).
        /// </summary>
        /// <param name="editing">If true, this will call the load method on the static map instead of the current map.</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static void LoadCustomCampaign(bool editing)
        {
            string mapName;

            if (editing)
            {
                mapName = "map-static.xml";
            }
            else
            {
                mapName = "map.xml";
            }

            FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Open, $"XAML File | {mapName}");

            GameBase.CurrentMapLocation = Path.GetDirectoryName(dialog.GetPath()) + "\\map.xml";
            GameBase.StaticMapLocation = Path.GetDirectoryName(dialog.GetPath()) + "\\map-static.xml";

            try
            {
                if (editing)
                {
                    GameBase.StaticGame = LoadMap(GameBase.StaticMapLocation);
                    GameBase.StaticGame.Load(GameBase.StaticGame); 
                }
                else
                {
                    GameBase.CurrentGame = LoadMap(GameBase.CurrentMapLocation);
                    GameBase.CurrentGame.Load(GameBase.CurrentGame);
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new campaign
        /// </summary>
        public static void NewCampaign()
        {
            try
            {
                GameBase.StaticGame = LoadMap(GameBase.StaticMapLocation);
                GameBase.CurrentGame = GameBase.StaticGame;

                GameBase.CurrentGame.Load(GameBase.CurrentGame);

                SaveCurrentMap();
            }
            catch (FileNotFoundException)
            {

                throw;
            }
        }

        /// <summary>
        /// Loads the campaign that came with the game.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadCampaign()
        {
            GameBase.CurrentMapLocation = Path.GetFullPath("..\\..\\..\\Campaign\\map.xml");
            GameBase.StaticMapLocation = Path.GetFullPath("..\\..\\..\\Campaign\\map-static.xml");

            if (!File.Exists(GameBase.CurrentMapLocation) || !File.Exists(GameBase.StaticMapLocation))
            {
                GameBase.CurrentMapLocation = null;
                GameBase.StaticMapLocation = null;
                throw new FileNotFoundException("CurrentMapLocation and/or StaticMapLocation could not be found! Have the exe file been moved from bin directory?");
            }

            try
            {
                GameBase.CurrentGame = LoadMap(GameBase.CurrentMapLocation);

                GameBase.CurrentGame.Load(GameBase.CurrentGame);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        /// <summary>
        /// Saves an object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void SaveObject(Type type, object obj, string path)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            try
            {
                using (TextWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, obj);
                }
            }
            catch (IOException)
            {
                MessageBoxResult result = MessageBox.Show("File cannot be saved! Retry?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);

                if (result == MessageBoxResult.Yes)
                    SaveObject(type, obj, path);
            }
        }

        public static void SaveAllMaps()
        {
            SaveStaticMap();
            SaveCurrentMap();
        }

        public static void SaveStaticMap()
        {
            GameBase.StaticGame.Unload(true);
            SaveObject(typeof(Map), GameBase.StaticGame, GameBase.StaticMapLocation);
        }

        public static void SaveCurrentMap()
        {
            GameBase.CurrentGame.Unload(false);
            SaveObject(typeof(Map), GameBase.CurrentGame, GameBase.CurrentMapLocation);
        }

        /// <summary>
        /// Loads a map from a save file.
        /// </summary>
        /// <param name="loadLocation">The location of the save file.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>The loaded map</returns>
        public static Map LoadMap(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                try
                {
                    Map map = (Map)serializer.Deserialize(fs);
                    return map;
                }
                catch (InvalidOperationException e)
                {
                    MessageBox.Show(
                        $"Error: Save file cannot be read! Advanced error message:\r\n{e}",
                        "Error: Save file cannot be read!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }

        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            string newPath = relativeUri.ToString().Replace("/", "\\");

            // Uri's contains uri escape codes. Replace them.
            newPath = Uri.UnescapeDataString(newPath);

            return newPath;
        }

        /// <summary>
        /// Copies a bitmap to the correct file directory.
        /// </summary>
        /// <returns>The string of the location of new bitmap relative to the project directory.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string SaveBitmap(ObjectOfAlbertrizal obj, string fileNameAddition)
        {
            try
            {
                FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Open, "PNG File | *.png");
                string fullPath = CopyImageToProjectDirectory(dialog.GetPath(), fileNameAddition, obj);
                string relativePath = GetRelativePath(fullPath, GameBase.CurrentMapLocation);
                return relativePath;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException();
            }
            catch (IOException e)
            {
                MessageBoxResult result =  MessageBox.Show("File cannot be read or is busy. Try again?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);

                if (result == MessageBoxResult.Yes)
                    return SaveBitmap(obj, fileNameAddition);
                else
                    return "";
            }
        }

        /// <summary>
        /// Copies an image to the project directory.
        /// </summary>
        /// <param name="location">The location of the file.</param>
        /// <param name="obj">The object being saved.</param>
        public static string CopyImageToProjectDirectory(string location, string fileNameAddition, ObjectOfAlbertrizal obj)
        {

            if (obj.Name == null || obj.Name == "")
            {
                MessageBox.Show("Please enter a name for your creation before selecting an image.");
                throw new ArgumentException("Object must be given a name");
            }

            string basetype = obj.GetType().ToString();
            basetype = basetype.Substring(basetype.LastIndexOf(".") + 1);

            string saveLocation = $"{Path.GetDirectoryName(GameBase.CurrentMapLocation)}\\{basetype}\\{obj.Name}_{fileNameAddition}.png";
            Directory.CreateDirectory(Path.GetDirectoryName(saveLocation));

            if (File.Exists(saveLocation))
            {
                MessageBoxResult result = MessageBox.Show($"{saveLocation} exists. Overwrite?", "File Exists", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                    return saveLocation;
            }

            File.Copy(location, saveLocation, true);

 
            return saveLocation;
        }

        public static void ClearTempDirectory()
        {
            try
            {
                string tempDirectory = $"{Path.GetTempPath()}RuinsOfAlbertrizal\\";
                Directory.Delete(tempDirectory, true);
            }
            catch (Exception)
            {

            }
        }
    }
}
