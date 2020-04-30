using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public abstract partial class EditorInterface : Page
    {
        protected List<Control> RequiredControls { get; set; }

        protected void Save(object sender, RoutedEventArgs e)
        {
            RequiredControls = new List<Control>();
            AddRequiredControls();

            if (!FormIsValid())
            {
                MessageBox.Show("Please fill out all required forms", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SafelyExit();
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        protected void Quit(object sender, RoutedEventArgs e)
        {
            ClearVariable();
            CreateMapPrompt.DoNotUpdate = true;
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Anything the program needs to do before navigating to CreateMaPrompt can be done here.
        /// </summary>
        protected virtual void SafelyExit()
        {

        }

        protected abstract void AddRequiredControls();

        protected abstract void UpdateComponent();

        protected bool FormIsValid()
        {
            foreach (Control control in RequiredControls)
            {
                if (!control.IsValid())
                    return false;
            }
            return true;
        }

        protected abstract void ClearVariable();

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        protected static string GetBitmapPath()
        {
            FileDialog dialog = new FileDialog(FileDialog.DialogOptions.Open, "PNG File | *.png");
            return dialog.GetPath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        protected static Bitmap OpenBitmap()
        {
            try
            {
                return (Bitmap)Bitmap.FromFile(GetBitmapPath());
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException();
            }
            catch (IOException)
            {
                MessageBox.Show("File cannot be read or is busy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">The expected width of the bitmap.</param>
        /// <param name="height">The expected height of the bitmap.</param>
        /// <returns></returns>
        protected static Bitmap OpenBitmap(int width, int height)
        {
            Bitmap bitmap = OpenBitmap();

            if (bitmap.Width != width || bitmap.Height != height)
            {
                MessageBox.Show($"Dimentions of image is incorrect. Must be {width} pixels long and {height} pixels tall.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new ArgumentException("Incorrect picture dimentions.");
            }
            else
                return bitmap;
        }
    }
}
