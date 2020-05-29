using RuinsOfAlbertrizal.Editor.AdderPrompts;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //Panel mainContainer = (Panel)this.Content;

            //StackPanel form = (StackPanel)mainContainer.FindName("Form");

            //if (form == null)
            //    throw new ArgumentException("There must exists a stackpanel of name form within any page that uses editor interface");

            //List<TextBox> allTextBoxes = form.Children.OfType<TextBox>().ToList();

            //foreach (TextBox textBox in allTextBoxes)
            //{
            //    if (textBox.Style.Equals(Resources["validationError"]))
            //        RequiredControls.Add(textBox);
            //}


            AddRequiredControls();

            if (!FormIsValid())
            {
                MessageBox.Show("Please fill out all required forms and check the requirements of each form.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        protected void EditBuffBtn_Click(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            List<Buff> buffs = (List<Buff>)control.Tag;
            BuffAdderPrompt buffAdderPrompt = new BuffAdderPrompt(buffs);
            buffAdderPrompt.ShowDialog();
            buffs = buffAdderPrompt.TargetBuffs;
        }

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
                EnumDescriptor descriptor = new EnumDescriptor((Array)imgElement.Tag);
                descriptor.Show();
            }
        }
    }
}
