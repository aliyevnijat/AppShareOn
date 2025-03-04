using System.ComponentModel.DataAnnotations;

namespace AppShareOn.Application.Dtos;

/// <summary>
/// Data transfer object for the Social Network platform that is used to fetch posts.
/// </summary>
public class PlatformDto
{
    /// <summary>
    /// Unique identifier for the platform.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifying name for the platform. (Ex. Instagram, Twitter etc.)
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Application id for the Platform.
    /// </summary>
    [StringLength(64)]
    public string? AppId { get; set; }

    /// <summary>
    /// URL Endpoint for the platform API. This endpoint 
    /// will be used to make requests and fetch posts.
    /// </summary>
    [Required]
    [StringLength(512)]
    public string ApiEndpoint { get; set; } = string.Empty;
}