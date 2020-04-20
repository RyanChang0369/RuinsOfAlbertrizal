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
        public RequiredValidation()
        { }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((string)value == null || (string)value == "")
            {
                return new ValidationResult(false, "Value is required.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
