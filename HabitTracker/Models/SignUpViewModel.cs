using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        public required string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public required string Lastname { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The entered password and confirm password do not match.")]
        public required string ConfirmPassword { get; set;}
    }
}
