using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppShareOn.Api.Controllers;

/// <summary>
/// Handles API requests for a profile.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    /// <summary>
    /// Logger for logging error, warning and info messages.
    /// </summary>
    private readonly ILogger<ProfilesController> _logger;

    /// <summary>
    /// Service to perform profile operations.
    /// </summary>
    private readonly IProfileService _profilesService;

    /// <summary>
    /// Instantiates a new instance of <see cref="ProfilesController"/>.
    /// </summary>
    /// <param name="logger">Logger for the <see cref="ProfilesController"/>.</param>
    /// <param name="profilesService">Service that handles profile operations.</param>
    public ProfilesController(ILogger<ProfilesController> logger,
        IProfileService profilesService)
    {
        _logger = logger;
        _profilesService = profilesService;
    }

    /// <summary>
    /// Gets the specific profile by id.
    /// </summary>
    /// <param name="id">Unique id for the profile.</param>
    /// <returns>Single matching profile.</returns>
    [HttpGet("{id}", Name = "GetProfileById")]
    public async Task<ActionResult<ApiResponse<ProfileDto>>> GetById(Guid id)
    {
        var result = await _profilesService.GetProfileByIdAsync(id);

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Creates a profile.
    /// </summary>
    /// <param name="createProfileDto">Data transfer object for creating a profile.</param>
    /// <returns><see cref="ProfileDto"/> for a newly created profile.</returns>
    [HttpPost(Name = "CreateProfile")]
    public async Task<ActionResult<ApiResponse<ProfileDto>>> Create([FromBody] CreateProfileDto createProfileDto)
    {
        // Validate.
        if (!ModelState.IsValid)
            return BadRequest(createProfileDto);

        var result = await _profilesService.CreateProfileAsync(createProfileDto);

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Updates profile details.
    /// </summary>
    /// <param name="updateProfileDto">Data transfer object for updating a profile.</param>
    /// <returns>Updated profile details.</returns>
    [HttpPut(Name = "UpdateProfile")]
    public async Task<ActionResult<ApiResponse<ProfileDto>>> Update([FromBody] UpdateProfileDto updateProfileDto)
    {
        // Validate.
        if (!ModelState.IsValid)
            return BadRequest(updateProfileDto);

        var result = await _profilesService.UpdateProfileAsync(updateProfileDto);

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }
}