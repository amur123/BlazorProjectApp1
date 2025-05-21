using System.ComponentModel.DataAnnotations;

namespace BlazorProjectApp1.Entities.ViewModel
{
    /// <summary>
    /// ViewModel for user login.
    /// For data binding in the login form.
    /// Contains properties for username and password with validation attributes.
    /// </summary>
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
