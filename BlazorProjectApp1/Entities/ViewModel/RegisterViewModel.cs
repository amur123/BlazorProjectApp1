using System.ComponentModel.DataAnnotations;

namespace BlazorProjectApp1.Entities.ViewModel
{
    /// <summary>
    /// ViewModel for user registration.
    /// Data transfer object for user registration form.
    /// Includes properties for username, password and role with validation attributes.
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
