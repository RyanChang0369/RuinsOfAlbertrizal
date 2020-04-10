using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using System;
using System.IO;
using System.Threading;
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

        public static object LoadObject()
        {
            string path = "";
            try
            {
                path = new FileDialog((int)FileDialog.DialogOptions.Load).GetPath();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return FileHandler.LoadObject(path);
        }

        public static object LoadObject(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(fs);
            }
        }

        public static void SaveObject(Type type, object obj, string path)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static Map LoadMap(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                return (Map)serializer.Deserialize(fs);
            }
        }

        public static void SaveEnemy(Enemy enemy, string saveLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Enemy));
            using (TextWriter writer = new StreamWriter(saveLocation))
            {
                serializer.Serialize(writer, enemy);
            }
        }

        public static Enemy LoadEnemy(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Enemy));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                return (Enemy)serializer.Deserialize(fs);
            }
        }

        public static void SavePlayer(Player player, string saveLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player));
            using (TextWriter writer = new StreamWriter(saveLocation))
            {
                serializer.Serialize(writer, player);
            }
        }

        public static Player LoadPlayer(string loadLocation)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player));
            using (FileStream fs = new FileStream(loadLocation, FileMode.Open))
            {
                return (Player)serializer.Deserialize(fs);
            }
        }
    }
}
