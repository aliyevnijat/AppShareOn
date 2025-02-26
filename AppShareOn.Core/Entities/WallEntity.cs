using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Core.Entities;

/// <summary>
/// Entity for a wall that displays posts.
/// </summary>
public class WallEntity : UpdatableEntity
{
    /// <summary>
    /// Identifying name for the <see cref="WallEntity"/>.
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// List of profiles for a given <see cref="WallEntity"/>.
    /// </summary>
    public virtual ICollection<ProfileEntity> Profiles { get; set; } = [];

    /// <summary>
    /// List of hashtags for a given <see cref="WallEntity"/>.
    /// </summary>
    public virtual ICollection<HashtagEntity> Hashtags { get; set; } = [];
}