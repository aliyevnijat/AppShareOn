using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Core.Entities;

/// <summary>
/// Entity for approved social media posts.
/// </summary>
public class PostEntity : BaseEntity
{
    /// <summary>
    /// URL for the post.
    /// </summary>
    [Required]
    [MaxLength(2000)]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Number of likes for the post.
    /// </summary>
    public int? Likes { get; set; }

    /// <summary>
    /// Number of comments for the post.
    /// </summary>
    public int? Comments { get; set; }

        /// <summary>
    /// Foreign key for the <see cref="PlatformEntity"/>.
    /// </summary>
    public Guid PlatformId { get; set; }

    /// <summary>
    /// Identifier for the post within the platform.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string PlatformPostId { get; set; } = string.Empty;

    /// <summary>
    /// Date and the time the post was made.
    /// </summary>
    public DateTime? PostedDateTime { get; set; }

    /// <summary>
    /// Navigation property for the <see cref="PlatformEntity"/>.
    /// </summary>
    public virtual PlatformEntity? Platform { get; set; }

    /// <summary>
    /// Navigation property for the list of associated <see cref="HashtagEntity"/>.
    /// </summary>
    public virtual ICollection<HashtagEntity> Hashtags { get; set; } = [];
}