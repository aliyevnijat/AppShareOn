using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Dtos;

/// <summary>
/// Data transfer object for creating a <see cref="ProfileDto"/>>.
/// </summary>
public class CreateProfileDto
{
    /// <summary>
    /// Unique identifier for the associated platform.
    /// </summary>
    public Guid PlatformId { get; set; }

    /// <summary>
    /// Handle for the profile in a specific social media platform.
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