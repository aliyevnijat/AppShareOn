using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Models.Account;

/// <summary>
/// Model representing the data required for user registration.
/// </summary>
public class RegisterModel
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// This will be used for login and communication.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
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

    /// <summary>
    /// Gets or sets the invite code, which may be required for registration.
    /// This is used for scenarios where users need an invitation to sign up.
    /// </summary>
    [StringLength(10, ErrorMessage = "Invite code is too long.")]
    public string InviteCode { get; set; } = string.Empty;
}