using System;
using System.ComponentModel.DataAnnotations;

namespace _1.Odev.CustomValidations
{
	public class MinAgeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => "not available";

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            //min start age = 7
            var minAge = DateTime.Now.AddYears(-7);

            return minAge < (DateTime)value ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }
    }
}

