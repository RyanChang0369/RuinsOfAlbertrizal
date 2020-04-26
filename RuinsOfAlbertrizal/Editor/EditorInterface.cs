using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public abstract partial class EditorInterface : Page
    {
        //public enum Mode
        //{
        //    /// <summary>
        //    /// Where this editor interface is not associated with any list.
        //    /// </summary>
        //    SingleVariable = -1,
        //    /// <summary>
        //    /// Where this editor interface adds (creates new) to a list of stored objects in Map.cs
        //    /// </summary>
        //    Add = 0,
        //    /// <summary>
        //    /// Where this editor interface replaces (edits existing) a stored object in Map.cs
        //    /// </summary>
        //    Replace = 1
        //}

        //public static Mode EditorMode { get; set; }

        public void ToggleMode()
        {
            switch (EditorMode)
            {
                case Mode.Add:
                    EditorMode = Mode.Replace;
                    break;
                case Mode.Replace:
                    EditorMode = Mode.Add;
                    break;
                default:
                    throw new NotSupportedException("Cannot toggle EditorMode in SingleVariable mode");
            }
        }

        protected void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Editor/CreateMapPrompt.xaml", UriKind.RelativeOrAbsolute));
        }

        protected abstract void UpdateComponent();


    }
}
