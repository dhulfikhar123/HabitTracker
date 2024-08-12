using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Input a valid password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
