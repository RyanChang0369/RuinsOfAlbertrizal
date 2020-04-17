using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static bool ValidateTextBoxes(TextBox[] requiredTextBoxes)
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
        public static bool ValidateTextBoxes(TextBox[] requiredTextBoxes,
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

        /// <summary>
        /// Parses the numerical boxes and returns the numerical values.
        /// </summary>
        /// <param name="numericalBoxes"></param>
        /// <returns></returns>
        public static int[] ParseNumericalValues(TextBox[] numericalBoxes)
        {
            if (numericalBoxes.Length != 5)
                throw new ArgumentException("Length of argument must be five");

            int[] numericalValues = new int[5];

            for (int i = 0; i < numericalBoxes.Length; i++)
            {
                numericalBoxes[i].Text = Regex.Replace(numericalBoxes[i].Text, "[^0-9]+", "");
                try
                {
                    numericalValues[i] = int.Parse(numericalBoxes[i].Text);
                }
                catch (Exception)
                {
                    numericalValues[i] = 0;
                }
            }

            return numericalValues;
        }
    }
}
