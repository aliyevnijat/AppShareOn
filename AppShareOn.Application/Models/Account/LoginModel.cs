using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Models.Account;

/// <summary>
/// Represents the login request model for user authentication.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// This is required for login authentication and must be a valid email format.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// This is required for login authentication and should be at least 6 characters long.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).*$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a flag indicating whether the user wants to be remembered for future logins.
    /// This is an optional property.
    /// </summary>
    public bool RememberMe { get; set; }
}