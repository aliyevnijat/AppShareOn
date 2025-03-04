namespace AppShareOn.Application.Dtos;

/// <summary>
/// TokenDto - Contains the user id and email confirmation token returned after login or registration
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Gets or sets the user id.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confirmation token for the user.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}