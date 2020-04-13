using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RuinsOfAlbertrizal.XMLInterpreter
{
    public class FileDialog
    {
        public enum DialogOptions
        {
            Save = 1,
            Open = 2,
            Load = 2,
            Folder = 3
        }
        private string Path;

        public FileDialog(int dialogOption)
        {
            switch (dialogOption)
            {
                case (int)DialogOptions.Open:
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    if (openFileDialog.ShowDialog() == true)
                    {
                        Path = openFileDialog.FileName;
                    }
                    break;

                case (int)DialogOptions.Save:
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Path = saveFileDialog.FileName;
                    }
                    break;

                case (int)DialogOptions.Folder:
                    SaveFileDialog folderDialog = new SaveFileDialog();
                    folderDialog.Title = "Select a Directory";
                    folderDialog.Filter = "Directory|*.this.directory";
                    folderDialog.FileName = "select";


                    if (folderDialog.ShowDialog() == true)
                    {
                        string path = folderDialog.FileName;
                        // Remove fake filename from resulting path
                        path = path.Replace("\\select.this.directory", "");
                        path = path.Replace(".this.directory", "");
                        Path = path;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        public string GetPath()
        {
            if (Path == null)
                throw new ArgumentNullException("Path cannot be null");

            return Path;
        }
    }
}