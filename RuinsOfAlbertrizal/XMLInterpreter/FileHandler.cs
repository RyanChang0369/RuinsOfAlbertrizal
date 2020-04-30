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

            //CreateProjectDirectory(dialog.GetPath());

            GameBase.NewGame(new Map());

            SaveObject(typeof(Map), GameBase.CurrentGame, GameBase.CustomMapLocation);
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
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("File cannot be saved! Retry?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);

                if (result == MessageBoxResult.Yes)
                    SaveObject(type, obj, path);
            }
        }

        /// <summary>
        /// Saves the object in default directory
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentNullException"></exception>
        //public static void SaveObject(Type type, object obj)
        //{
        //    SaveObject(type, obj, GameBase.CustomMapLocation);
        //}

        public static Map LoadMap(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                return (Map)serializer.Deserialize(fs);
            }
        }
    }
}
