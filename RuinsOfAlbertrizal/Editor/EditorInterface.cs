using RuinsOfAlbertrizal.XMLInterpreter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

        protected void ComboBox_ChangeTooltip(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string tooltip = "";

            try
            {
                //Fix
                Enum enumValue = (Enum)comboBox.Items.SourceCollection;

                FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    tooltip =  attributes[0].Description;
                else
                    tooltip = "Select item to view description";
            }
            catch (Exception)
            {
                tooltip = "Select item to view description";
            }

            comboBox.ToolTip = tooltip;
        }
    }
}
