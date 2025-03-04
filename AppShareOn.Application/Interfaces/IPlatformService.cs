using AppShareOn.Application.Dtos;
using AppShareOn.Application.Models;
using AppShareOn.Core.Entities;

namespace AppShareOn.Application.Interfaces;

/// <summary>
/// Handles all platform operations.
/// </summary>
public interface IPlatformService
{
    /// <summary>
    /// Gets a list of <see cref="PlatformEntity"/> and maps it to
    /// a list of <see cref="PlatformDto"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="ApiResponse{IEnumerable{PlatformDto}}"/> with list of <see cref="PlatformDto"/>.
    /// </returns>
    Task<ApiResponse<IEnumerable<PlatformDto>>> GetPlatformsAsync();

    /// <summary>
    /// Gets a <see cref="PlatformEntity"/> by id and maps it 
    /// to a <see cref="PlatformDto"/>.
    /// </summary>
    /// <param name="id">Unique identifier for the platform.</param>
    /// <returns>
    /// An <see cref="ApiResponse{PlatformDto}"/> with a single <see cref="PlatformDto"/>.
    /// </returns>
    Task<ApiResponse<PlatformDto>> GetPlatformByIdAsync(Guid id);

    /// <summary>
    /// Maps the <see cref="PlatformDto"/> to a <see cref="PlatformEntity"/> 
    /// and saves it to the database.
    /// </summary>
    /// <param name="platformDto">Data transfer object for updating a platform.</param>
    /// <returns>
    /// An <see cref="ApiResponse{PlatformDto}"/> with a single <see cref="PlatformDto"/> for an updated platform.
    /// </returns>
    Task<ApiResponse<PlatformDto>> UpdatePlatformAsync(PlatformDto platformDto);
}