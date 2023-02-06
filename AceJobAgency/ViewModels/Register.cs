using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;


namespace AceJobAgency.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[STFGstfg]\d{7}[a-zA-Z]$",
            ErrorMessage = "Invalid NRIC")]
        public string NRIC { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(12, ErrorMessage = "Enter at least a 12 characters password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,18}$",
            ErrorMessage = "Passwords must be at least 12 characters long and contain at least an upper case letter, lower case letter, digit and a symbol")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [MinimumAgeAttribute(18)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Enter at least a 12 characters password")]
        [DataType(DataType.Text)]
        public string WhoamI { get; set; }


        [Required]
        public IFormFile? Resume { get; set; }
    }
}
