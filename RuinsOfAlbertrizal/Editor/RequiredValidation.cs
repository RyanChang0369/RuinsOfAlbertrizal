using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public class RequiredValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((string)value == null || (string)value == "")
            {
                return new ValidationResult(false, $"Box not filled in.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
