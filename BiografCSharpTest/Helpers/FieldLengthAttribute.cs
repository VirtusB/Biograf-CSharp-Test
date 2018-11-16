using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BiografCSharpTest.Helpers
{
    public sealed class FieldLengthAttribute : ValidationAttribute
    {
        private int _minValue { get; set; }
        private int _maxValue { get; set; }
        public FieldLengthAttribute(int minValue,int maxValue) {

            _minValue = minValue;
            _maxValue = maxValue;

            ErrorMessage = "{0} length should be between {1} and {2}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value != null)
            {
                int objectLength = Convert.ToString(value).Length;
                if (objectLength < _minValue || objectLength > _maxValue)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name,_minValue,_maxValue);
        }

    }
}