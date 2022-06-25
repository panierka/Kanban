using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Kanban.View.Validators
{
    internal class DateTimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string ?? string.Empty;

            if (!Regex.Match(text, @"[0-9]{2}-[0-9]{2}-[0-9]{4}").Success)
            {
                return new ValidationResult(false, 
                    "This format is not supported. Use dd-mm-yyyy, for example: 25-03-2001");
            }

            if (!DateTime.TryParse(text, out DateTime _))
            {
                return new ValidationResult(false,
                    "This is not a proper date.");
            }
            
            return ValidationResult.ValidResult;
        }
    }
}
