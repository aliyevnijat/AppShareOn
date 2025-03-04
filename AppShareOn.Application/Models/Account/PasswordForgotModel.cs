using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Models.Account;

/// <summary>
/// Represents the "PASSWORD RESET REQUEST" model for account recovery.
/// </summary>
public class PasswordForgotModel
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// This is required for password reset and must be a valid email format.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;
}