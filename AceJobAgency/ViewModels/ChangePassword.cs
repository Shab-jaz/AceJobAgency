using System.ComponentModel.DataAnnotations;

namespace AceJobAgency.ViewModels
{
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "New Password and confirmation password do not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
