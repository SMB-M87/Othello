using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC.Areas.Identity.Pages.Account
{
    public class NormalizedPassword : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string password || string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("Password is required.");
            }

            if (password.Length > 64)
            {
                return new ValidationResult("Password is not allowed to exceed 64 characters.");
            }

            var normalizedPassword = Regex.Replace(password.Trim(), @"\s+", " ");

            if (normalizedPassword.Length < 16)
            {
                return new ValidationResult("Password must be at least 16 characters long.");
            }

            if (PasswordBreach.IsPasswordBreached(password))
            {
                return new ValidationResult("The password you entered has been found in a data breach. Please choose a different password.");
            }

            if (!Regex.IsMatch(normalizedPassword, @"^[\p{L}\p{N}\p{P}\p{S}\p{Zs}\uD83C-\uDBFF\uDC00-\uDFFF]+$"))
            {
                return new ValidationResult("Password contains invalid characters.");
            }

            return ValidationResult.Success!;
        }
    }
}
