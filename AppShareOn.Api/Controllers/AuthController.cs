using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using AppShareOn.Application.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace AppShareOn.Api.Controllers;

/// <summary>
/// Handles user authentication operations including registration, 
/// confirmation, login, and logout.
/// </summary>
/// <remarks>
/// The controller uses <see cref="AuthService"/> to perform operations such as 
/// creating users, authenticating users, and signing users in or out.
/// </remarks>
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service that handles user registration and login.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService), $"{nameof(authService)} cannot be null.");
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An ApiResponse with the registration result.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<object>>> Register([FromBody] RegisterModel model)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "User registration failed."));

        var response = await _authService.RegisterAsync(model);

        // If failed
        if (!response.Success)
        {
            return BadRequest(response);
        }

        // Success
        return Ok(response);
    }

    /// <summary>
    /// Gets user information.
    /// </summary>
    /// <param name="id">Unique identifier for the user.</param>
    /// <returns><see cref="ApiResponse{UserDto}"/> for the user.</returns>
    [HttpGet("users/{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(string id)
    {
        var result = await _authService.GetUserByIdAsync(id);

        if (result == null)
            return NotFound(ApiResponse<UserDto>.ErrorResponse(["Failed to retreive user data."]));

        return Ok(ApiResponse<UserDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Validates user provided token and confirms user.
    /// </summary>
    /// <param name="id">Unique identifier for the user.</param>
    /// <param name="token">Confirmation token from email.</param>
    /// <returns>Success or failure as a result of confirmation process.</returns>
    [HttpGet("users/{id}/confirm")]
    public async Task<ActionResult<ApiResponse<object>>> Confirm(string id, [FromQuery] string token)
    {
        var result = await _authService.ConfirmUserAsync(id, token);

        if (!result.Succeeded)
            return NotFound(ApiResponse<object>.ErrorResponse(result.Errors.Select(x => x.Description), "Failed to confirm user."));

        return Ok(ApiResponse<object>.SuccessResponse("User is confirmed."));
    }

    /// <summary>
    /// Logs in an existing user and returns an authentication token.
    /// </summary>
    /// <param name="model">The login details provided by the client.</param>
    /// <returns>An ApiResponse with the login result.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginModel model)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "Login failed."));

        // Call the AuthService to handle login logic
        var response = await _authService.LoginAsync(model);

        if (response != null)
            return Ok(ApiResponse<AuthTokenDto>.SuccessResponse(response, "Login successful."));

        return Unauthorized(ApiResponse<object>.ErrorResponse(["Failed to authenticate user"], "Login failed."));
    }

    /// <summary>
    /// Handles password reset requests.
    /// </summary>
    /// <param name="email">User's email for password reset.</param>
    /// <returns>An ApiResponse with the password reset result.</returns>
    [HttpPost("forgot-password")]
    public async Task<ActionResult<ApiResponse<object>>> RequestPasswordReset([FromBody] PasswordForgotModel model)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "Password reset failed."));

        // Call the AuthService to handle password reset.
        var response = await _authService.RequestPasswordResetAsync(model.Email);

        if (response == null)
            return BadRequest(ApiResponse<string>.ErrorResponse(["Failed to process the request for password reset."], "Password reset failed."));

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Processes password reset with the user provided token and new password.
    /// </summary>
    /// <param name="model">Model that encapsulates user provided details such as email and new password.</param>
    /// <param name="token">User provided password reset token.</param>
    /// <returns>An <see cref="ApiResponse{object}"/> with the password reset result.</returns>
    [HttpPost("reset-password")]
    public async Task<ActionResult<ApiResponse<object>>> ProcessPasswordReset([FromBody] PasswordResetModel model, [FromQuery] string token)
    {
        // Check model data.
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.ErrorResponse(["Invalid input data"], "Password reset failed."));

        // Call the AuthService to handle password reset.
        var response = await _authService.ProcessPasswordResetAsync(model, token);

        // If password reset fails to process.
        if (response == null)
            return BadRequest(ApiResponse<string>.ErrorResponse(["Failed to process the request for password reset."], "Password reset failed."));

        // If passwprd reset is not succesful.
        if (!response.Success)
            return BadRequest(response);

        // Password reset success.
        return Ok(response);
    }
}