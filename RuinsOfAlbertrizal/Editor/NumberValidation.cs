using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public class NumberValidation : ValidationRule
    {
        public NumberValidation()
        { }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;

            try
            {
                if (((string)value).Length > 0)
                    number = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
