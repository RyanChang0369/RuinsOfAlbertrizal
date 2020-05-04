using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor.Validator
{
    /// <summary>
    /// Validates a percent as a double (0.0 - 1.0)
    /// </summary>
    public class PercentValidation : ValidationRule
    {
        public PercentValidation()
        { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double number = 0.0;

            try
            {
                if (((string)value).Length > 0)
                    number = double.Parse((string)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Value entered must be a number.");
            }

            if (number < 0.0 || number > 1.0)
                return new ValidationResult(false, "Value must be between 0.0 and 1.0");

            return ValidationResult.ValidResult;
        }
    }
}
