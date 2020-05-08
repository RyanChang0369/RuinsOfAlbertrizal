using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor.Validator
{
    public class SelectedIndexValidation : ValidationRule
    {
        public SelectedIndexValidation()
        { }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((int)value < 0)
                return new ValidationResult(false, "Please select a value");
            else
                return ValidationResult.ValidResult;
        }
    }
}
