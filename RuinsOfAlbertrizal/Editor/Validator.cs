using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public static class Validator
    {
        /// <summary>
        /// Returns true if validation succeeds
        /// </summary>
        /// <returns></returns>
        public static bool Validate(TextBox[] requiredTextBoxes)
        {
            foreach (TextBox box in requiredTextBoxes)
            {
                if (box.Text == null || box.Text == "")
                {
                    MessageBox.Show("Please fill out all required text boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Returns true if validation succeeds
        /// </summary>
        /// <returns></returns>
        public static bool Validate(TextBox[] requiredTextBoxes,
            ComboBox[] requiredComboBoxes)
        {
            foreach (TextBox box in requiredTextBoxes)
            {
                if (box.Text == null || box.Text == "")
                {
                    MessageBox.Show("Please fill out all required text boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            foreach (ComboBox box in requiredComboBoxes)
            {
                if (box.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill out all required combo boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }
}
