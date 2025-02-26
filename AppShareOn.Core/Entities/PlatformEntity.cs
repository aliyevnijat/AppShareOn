using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Core.Entities;

/// <summary>
/// Type of Social Network service that is used to fetch posts.
/// </summary>
public class PlatformEntity : UpdatableEntity
{
    /// <summary>
    /// Identifying name for the <see cref="PlatformEntity"/>. (Ex. Instagram, Twitter etc.)
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Application id for the <see cref="PlatformEntity" />.
    /// </summary>
    [StringLength(64)]
    public string? AppId { get; set; }

    /// <summary>
    /// URL Endpoint for the <see cref="PlatformEntity"/>'s API. This endpoint 
    /// will be used to make requests and fetch posts.
    /// </summary>
    [Required]
    [StringLength(512)]
    public string ApiEndpoint { get; set; } = string.Empty;
}