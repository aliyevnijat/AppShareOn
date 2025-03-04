using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using AppShareOn.Core.Entities;
using AppShareOn.Core.Interfaces;

namespace AppShareOn.Application.Services;

/// <summary>
/// Handles all platform operations.
/// </summary>
public class PlatformService : IPlatformService
{
    /// <summary>
    /// Unit of work for database operations.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Repository to access platform records.
    /// </summary>
    private readonly IRepository<PlatformEntity> _platformRepository;

    /// <summary>
    /// Instantiates a new instance of <see cref="PlatformService">.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for repository and database access.</param>
    public PlatformService(IUnitOfWork unitOfWork)
    {
        _platformRepository = unitOfWork.Repository<IRepository<PlatformEntity>>();
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<PlatformDto>> GetPlatformByIdAsync(Guid id)
    {
        // Get platform from the repository.
        var platform = await _platformRepository.GetByIdAsync(id);

        if (platform == null)
            return ApiResponse<PlatformDto>.ErrorResponse(["Platform record cannot be found."]);
            
        // Map the platform to dto.
        var platformDto = new PlatformDto
        {
            Id = platform.Id,
            Name = platform.Name,
            AppId = platform.AppId,
            ApiEndpoint = platform.ApiEndpoint
        };

        return ApiResponse<PlatformDto>.SuccessResponse(platformDto);
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<IEnumerable<PlatformDto>>> GetPlatformsAsync()
    {
        // Get platforms from the database.
        var platforms = await _platformRepository.GetAllAsync();

        // Map list of platforms to dto.
        var platformsDto = platforms.Select(x => new PlatformDto {
            Id = x.Id,
            Name = x.Name,
            AppId = x.AppId,
            ApiEndpoint = x.ApiEndpoint
        });

        return ApiResponse<IEnumerable<PlatformDto>>.SuccessResponse(platformsDto);
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<PlatformDto>> UpdatePlatformAsync(PlatformDto platformDto)
    {
        // Get platform from the repository
        var platform = await _platformRepository.GetByIdAsync(platformDto.Id);

        if (platform == null)
            throw new ArgumentException("Platform record cannot be found.");

        // Map platform dto to a platform entity.
        platform.AppId = platformDto.AppId;
        platform.Name = platformDto.Name;
        platform.ApiEndpoint = platformDto.ApiEndpoint;

        // Update platform entity and save to database.
        _platformRepository.Update(platform);
        await _unitOfWork.SaveAsync();

        // Get the platform from the database and return.
        return await GetPlatformByIdAsync(platformDto.Id);
    }
}