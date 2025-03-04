using AppShareOn.Application.Dtos;
using AppShareOn.Application.Models;
using AppShareOn.Core.Entities;

namespace AppShareOn.Application.Interfaces;

/// <summary>
/// Handles all profile operations.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// Gets a single <see cref="ProfileEntity"/> by id and maps
    /// it to <see cref="ProfileDto"/>.
    /// </summary>
    /// <param name="id">Unique identifier for the profile.</param>
    /// <returns><see cref="ApiResponse{ProfileDto}"/>.</returns>
    Task<ApiResponse<ProfileDto>> GetProfileByIdAsync(Guid id);

    /// <summary>
    /// Maps the <see cref="CreateProfileDto"/> to a <see cref="ProfileEntity"/>
    /// and saves it to the database.
    /// </summary>
    /// <param name="createProfileDto">Data transfer object for creating a profile.</param>
    /// <returns><see cref="ApiResponse{ProfileDto}"/> for the newly created <see cref="ProfileEntity"/>.</returns>
    Task<ApiResponse<ProfileDto>> CreateProfileAsync(CreateProfileDto createProfileDto);

    /// <summary>
    /// Maps the <see cref="UpdateProfileDto"/> to a <see cref="ProfileEntity"/> 
    /// and saves it to the database.
    /// </summary>
    /// <param name="updateProfileDto">Data transfer object for updating a profile.</param>
    /// <returns><see cref="ApiResponse{ProfileDto}"/> for an updated profile.</returns>
    Task<ApiResponse<ProfileDto>> UpdateProfileAsync(UpdateProfileDto updateProfileDto);

    // /// <summary>
    // /// Gets all posts for a profile and maps to a list of <see cref="PostDto"/>.
    // /// </summary>
    // /// <param name="id">Unique identifier for the profile.</param>
    // /// <returns>List of <see cref="PostDto"/>.</returns>
    // Task<IEnumerable<PostDto>> GetPostsForProfile(Guid id);

    // /// <summary>
    // /// Connects to platforms and gets posts for all hashtags within a profile
    // /// and saves them to the database.
    // /// </summary>
    // /// <param name="profileId">Unique identifier for the profile.</param>
    // /// <returns><see cref="Task"/>.</returns>
    // Task UpdatePostsForProfile(Guid profileId);
}