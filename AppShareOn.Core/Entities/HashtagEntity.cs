using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Core.Entities;

/// <summary>
/// Hashtag entity for a to pull the associated posts. 
/// </summary>
public class HashtagEntity : UpdatableEntity
{
    /// <summary>
    /// The title/content for the hashtag.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Platform specific id.
    /// </summary>
    [MaxLength(64)]
    public string? PlatformHashtagId { get; set; }

    /// <summary>
    /// Navigation property for the associated list of <see cref="PostEntity"/>.
    /// </summary>
    public virtual ICollection<PostEntity> Posts { get; set; } = [];

    /// <summary>
    /// List of walls for a given <see cref="HashtagEntity"/>.
    /// </summary>
    public virtual ICollection<WallEntity> Walls { get; set; } = [];
}