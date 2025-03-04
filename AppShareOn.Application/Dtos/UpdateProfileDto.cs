using AppShareOn.Application.Dtos;

/// <summary>
/// Data transfer object for updating the profile.
/// </summary>
public class UpdateProfileDto : CreateProfileDto
{
    /// <summary>
    /// Unique identifier for the profile.
    /// </summary>
    public Guid Id { get; set; }
}