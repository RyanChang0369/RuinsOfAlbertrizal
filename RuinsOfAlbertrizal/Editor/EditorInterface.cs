using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        protected void ComboBox_Initialize(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();

            foreach (Enum enumValue in comboBox.Items)
            {
                string tooltip = enumValue.GetDescription();

                ComboBoxItem comboBoxItem = new ComboBoxItem
                {
                    Content = enumValue,
                    ToolTip = tooltip
                };

                comboBoxItems.Add(comboBoxItem);
            }


            comboBox.ItemsSource = null;

            foreach (ComboBoxItem item in comboBoxItems)
            {
                comboBox.Items.Add(item);
            }
        }

        //protected void ComboBox_ChangeTooltip(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    string tooltip = "";

        //    //try
        //    //{
        //    //    //Fix
        //    //    Enum enumValue = (Enum)comboBox.Items.SourceCollection;

        //    //    FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

        //    //    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
        //    //        typeof(DescriptionAttribute), false);

        //    //    if (attributes != null && attributes.Length > 0)
        //    //        tooltip =  attributes[0].Description;
        //    //    else
        //    //        tooltip = "Select item to view description";
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    tooltip = "Select item to view description";
        //    //}

        //    //comboBox.ToolTip = tooltip;
        //}


        protected void InfoLoaded_EnumDescription(object sender, RoutedEventArgs e)
        {
            Image imgElement = (Image)sender;
            imgElement.ToolTip = "Click to open description for the below Combo Box.";

            imgElement.MouseUp += InfoMouseUp_EnumDescription;
        }

        private void InfoMouseUp_EnumDescription(object sender, MouseEventArgs e)
        {
            Image imgElement = (Image)sender;

            if (e.LeftButton == MouseButtonState.Released)
            {
                //Array enumValues = Enum.GetValues(((Enum)imgElement.Tag).GetType());

                Type test = imgElement.Tag.GetType();

                EnumDescriptor descriptor = new EnumDescriptor((Array)imgElement.Tag);
                descriptor.Show();
            }
        }
    }
}
