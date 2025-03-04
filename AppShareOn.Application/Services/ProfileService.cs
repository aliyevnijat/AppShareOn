using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using AppShareOn.Core.Entities;
using AppShareOn.Core.Interfaces;

namespace AppShareOn.Application.Services;

/// <summary>
/// Handles all profile operations.
/// </summary>
public class ProfileService : IProfileService
{
    /// <summary>
    /// Unit of work for database operations.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Repository to access profile records.
    /// </summary>
    private readonly IRepository<ProfileEntity> _profileRepository;

    /// <summary>
    /// Repository to access platform records.
    /// </summary>
    private readonly IRepository<PlatformEntity> _platformRepository;

    /// <summary>
    /// Instantiates a new instance of <see cref="ProfileService">.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for repository and database access.</param>
    public ProfileService(IUnitOfWork unitOfWork)
    {
        _profileRepository = unitOfWork.Repository<IRepository<ProfileEntity>>();
        _platformRepository = unitOfWork.Repository<IRepository<PlatformEntity>>();
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<ProfileDto>> CreateProfileAsync(CreateProfileDto createProfileDto)
    {
        // Get assocaited platform.
        var platform = await _platformRepository.GetByIdAsync(createProfileDto.PlatformId);

        // Confirm associated platform exists.
        if (platform == null)
            return ApiResponse<ProfileDto>.ErrorResponse(["Platform record could not be found for the provided platform id."]);

        // Get current date-time in UTC.
        var currentDateTime = DateTime.UtcNow;

        // Map profile dto to a profile entity.
        var profileEntity = new ProfileEntity
        {
            PlatformId = createProfileDto.PlatformId,
            ProfileHandle = createProfileDto.ProfileHandle,
            PlatformUserId = createProfileDto.PlatformUserId,
            Token = createProfileDto.Token,
            CreatedDateTime = currentDateTime
        };

        // Add and save profile entity.
        await _profileRepository.AddAsync(profileEntity);
        await _unitOfWork.SaveAsync();

        // Get the newly created profile from the database and return dto.
        return await GetProfileByIdAsync(profileEntity.Id);
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<ProfileDto>> GetProfileByIdAsync(Guid id)
    {
        // Get platform from the repository.
        var profile = await _profileRepository.GetByIdAsync(id, ["Platform"]);

        if (profile == null)
            return ApiResponse<ProfileDto>.ErrorResponse(["Profile record cannot be found."]);

        if (profile.Platform == null)
            return ApiResponse<ProfileDto>.ErrorResponse(["Associated platform cannot be found for the profile."]);
            
        // Map the platform to dto.
        var profileDto = new ProfileDto
        {
            Id = profile.Id,
            ProfileHandle = profile.ProfileHandle,
            PlatformUserId = profile.PlatformUserId,
            Token = profile.Token,
            Platform = new PlatformDto {
                Id = profile.PlatformId,
                Name = profile.Platform.Name,
                ApiEndpoint = profile.Platform.ApiEndpoint,
                AppId = profile.Platform.AppId
            },
        };

        return ApiResponse<ProfileDto>.SuccessResponse(profileDto);
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<ProfileDto>> UpdateProfileAsync(UpdateProfileDto updateProfileDto)
    {
        // Get associated platform.
        var platform = await _platformRepository.GetByIdAsync(updateProfileDto.PlatformId);

        // Confirm associated platform exists.
        if (platform == null)
            return ApiResponse<ProfileDto>.ErrorResponse(["Platform record could not be found for the provided platform id."]);

        // Get profile from the repository
        var profile = await _profileRepository.GetByIdAsync(updateProfileDto.Id, includeProperties: ["Platform"]);

        if (profile == null)
            return ApiResponse<ProfileDto>.ErrorResponse(["Profile record cannot be found."]);

        // Map profile dto to a profile entity.
        profile.ProfileHandle = updateProfileDto.ProfileHandle;
        profile.PlatformUserId = updateProfileDto.PlatformUserId;
        profile.Token = updateProfileDto.Token;
        profile.PlatformId = updateProfileDto.PlatformId;

        // Update profile entity and save to database.
        _profileRepository.Update(profile);
        await _unitOfWork.SaveAsync();

        // Get the profile from the database and return.
        return await GetProfileByIdAsync(updateProfileDto.Id);
    }
}