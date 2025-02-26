using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppShareOn.Api.Controllers;

/// <summary>
/// Handles API requests for Platforms.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    /// <summary>
    /// Logger for logging error, warning and info messages.
    /// </summary>
    private readonly ILogger<PlatformsController> _logger;

    /// <summary>
    /// Service for all platform operations.
    /// </summary>
    private readonly IPlatformService _platformService;

    /// <summary>
    /// Instantiates a new instance of <see cref="PlatformsController"/>.
    /// </summary>
    /// <param name="platformService">Service that handles platform operations.</param>
    /// <param name="logger">Logger for <see cref="PlatformsController"/>.</param>
    public PlatformsController(IPlatformService platformService,
        ILogger<PlatformsController> logger)
    {
        _platformService = platformService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all platforms from the database.
    /// </summary>
    /// <returns>List of platforms.</returns>
    [HttpGet(Name = "GetAllPlatforms")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PlatformDto>>>> GetAll()
    {
        var result = await _platformService.GetPlatformsAsync();

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Gets a specific platform by id.
    /// </summary>
    /// <param name="id">Unique id for the platform.</param>
    /// <returns>Single matching platform.</returns>
    [HttpGet("{id}", Name="GetPlatformById")]
    public async Task<ActionResult<ApiResponse<PlatformDto>>> GetById(Guid id)
    {
        var result = await _platformService.GetPlatformByIdAsync(id);

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Updates platform details.
    /// </summary>
    /// <param name="platformDto">Data transfer object for updating a platform.</param>
    /// <returns>Updated platform details.</returns>
    [HttpPut(Name = "UpdatePlatform")]
    public async Task<IActionResult> Update([FromBody] PlatformDto platformDto)
    {
        // Validate
        if (!ModelState.IsValid)
            return BadRequest(platformDto);

        var result = await _platformService.UpdatePlatformAsync(platformDto);

        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }
}