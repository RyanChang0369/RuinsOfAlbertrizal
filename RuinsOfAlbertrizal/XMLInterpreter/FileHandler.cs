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
            FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Save, "XAML File|.xml", "map");

            GameBase.CustomMapLocation = dialog.GetPath();

            GameBase.NewGame(new Map());

            SaveCurrentMap();
        }


        /// <summary>
        /// Opens a file dialog to load a custom map.
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LoadCustomCampaign()
        {
            FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Open, "XAML File | map.xml");

            GameBase.CustomMapLocation = dialog.GetPath();

            GameBase.NewGame(LoadMap(dialog.GetPath()));
        }

        /// <summary>
        /// Creates a project file with a path.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="IOException"></exception>
        public static void CreateProjectDirectory(string path)
        {
            string directory = Path.GetDirectoryName(path);

            Directory.CreateDirectory(directory + "/Character");
            Directory.CreateDirectory(directory + "/Environment");
            Directory.CreateDirectory(directory + "/Items");
            Directory.CreateDirectory(directory + "/Mechanics");
            Directory.CreateDirectory(directory + "/Text");
        }

        public static object LoadObject()
        {
            string path = "";
            try
            {
                path = new FileDialog(FileDialog.DialogOptions.Load).GetPath();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return LoadObject(path);
        }

        public static object LoadObject(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(fs);
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

        public static void SaveCurrentMap()
        {
            SaveObject(typeof(Map), GameBase.CurrentGame, GameBase.CustomMapLocation);
        }

        public static Map LoadMap(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                return (Map)serializer.Deserialize(fs);
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
                string relativePath = GetRelativePath(fullPath, GameBase.CustomMapLocation);
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

        ///// <summary>
        ///// When something is renamed, reflect that change in the file directories.
        ///// </summary>
        ///// <param name="oldName"></param>
        ///// <param name="newName"></param>
        //public static void AlertRename(string oldName, string newName, ObjectOfAlbertrizal obj)
        //{
        //    if (oldName == newName)
        //        return;

        //    string imageLocation = 
        //}

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

            string saveLocation = $"{Path.GetDirectoryName(GameBase.CustomMapLocation)}\\{basetype}\\{obj.Name}_{fileNameAddition}.png";
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
    }
}
