using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomerServiceWebClient.Validators
{

    public class ValidPhoneNumberAttribute : ValidationAttribute
    {
        private static readonly Regex PhoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !PhoneRegex.IsMatch(value.ToString()))
            {
                return new ValidationResult("Invalid phone number format.");
            }

            return ValidationResult.Success;
        }
    }

}
