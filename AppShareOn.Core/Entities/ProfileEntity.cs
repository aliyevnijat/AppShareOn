using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Core.Entities;

/// <summary>
/// Profile for the social network service in <see cref="PlatformEntity"/>.
/// </summary>
public class ProfileEntity : UpdatableEntity
{
    /// <summary>
    /// Foreign key for the <see cref="PlatformEntity"/>.
    /// </summary>
    public Guid PlatformId { get; set; }

    /// <summary>
    /// Navigation property for the <see cref="PlatformEntity"/>.
    /// </summary>
    public virtual PlatformEntity? Platform { get; set; }

    /// <summary>
    /// Handle for the social network <see cref="PlatformEntity"/>.
    /// </summary>
    [Required]
    [StringLength(64)]
    public string ProfileHandle { get; set; } = string.Empty;

    /// <summary>
    /// User id for the <see cref="ProfileEntity" within the platform />.
    /// </summary>
    [StringLength(64)]
    public string? PlatformUserId { get; set; }

    /// <summary>
    /// Token that is used to access <see cref="PlatformEntity"/> posts 
    /// for a <see cref="ProfileEntity"/>.
    /// </summary>
    [Required]
    [StringLength(512)]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// List of walls associated with <see cref="ProfileEntity"/>.
    /// </summary>
    public virtual ICollection<WallEntity> Walls { get; set; } = [];
}