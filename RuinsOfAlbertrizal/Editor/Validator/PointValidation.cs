using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RuinsOfAlbertrizal.Editor.Validator
{
    public class PointValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Point point = (Point)value;
                return ValidationResult.ValidResult;
            }
            catch (InvalidCastException)
            {
                return new ValidationResult(false, "Please enter in two numbers seperated by commas.");
            }
        }
    }
}
