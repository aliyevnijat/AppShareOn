using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Dtos;

/// <summary>
/// Data transfer object for a profile within the social network platform.
/// </summary>
public class ProfileDto
{
    /// <summary>
    /// Unique identifier for the profile.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Social media platform associated with the profile.
    /// </summary>
    [Required]
    public PlatformDto Platform { get; set; } = new PlatformDto();

    /// <summary>
    /// Handle for a profile within the social network platform.
    /// </summary>
    [Required]
    [StringLength(64)]
    public string ProfileHandle { get; set; } = string.Empty;

    /// <summary>
    /// User id for the profile within the platform.
    /// </summary>
    [StringLength(64)]
    public string? PlatformUserId { get; set; }

    /// <summary>
    /// Token that is used to access platform posts for a profile.
    /// </summary>
    [Required]
    [StringLength(512)]
    public string Token { get; set; } = string.Empty;
}