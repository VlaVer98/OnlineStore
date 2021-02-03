using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class ChangingPasswordViewModel
    {
        [Required]
        [DataType("Password")]
        public string OldPassword { get; set; }
        [Required]
        [DataType("Password")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType("Password")]
        public string ConfirmPassword { get; set; }
    }
}
