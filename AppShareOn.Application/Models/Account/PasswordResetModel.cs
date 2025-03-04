using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Models.Account;

/// <summary>
/// Represents the "PASSWORD RESET SUBMISSION" model for account recovery.
/// </summary>
public class PasswordResetModel
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// This is required for password reset and must be a valid email format.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// The password must meet security requirements, such as length and complexity.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).*$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confirmation password.
    /// This field is used to verify that the user entered the same password twice.
    /// </summary>
    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}