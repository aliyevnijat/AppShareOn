using AppShareOn.Application.Dtos;
using AppShareOn.Application.Models;
using AppShareOn.Application.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AppShareOn.Application.Interfaces;

/// <summary>
/// Interface for handling user authentication-related operations such as registration and login.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user, generates an authentication token and sends email.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An <see cref="ApiResponse{string}"/> containing the result of the registration process.</returns>
    Task<ApiResponse<string>> RegisterAsync(RegisterModel model);

    /// <summary>
    /// Generates password reset token and sends email to the user.
    /// </summary>
    /// <param name="email">User's email for password reset.</param>
    /// <returns>An <see cref="ApiResponse{object}"/> containing the result of the reset process.</returns>
    Task<ApiResponse<object>> RequestPasswordResetAsync(string email);

    /// <summary>
    /// Resets user's password using the given token.
    /// </summary>
    /// <param name="model">Model with the user data such as email and new password.</param>
    /// <param name="token">Password reset token provided by the user.</param>
    /// <returns>An <see cref="ApiResponse{object}"/> containing the result of the reset process.</returns>
    Task<ApiResponse<object>> ProcessPasswordResetAsync(PasswordResetModel model, string token);

    /// <summary>
    /// Generates a confirmation link for the unconfirmed user.
    /// </summary>
    /// <param name="email">Email for the unconfirmed user.</param>
    /// <returns><see cref="TokenDto"/>> object containing user information and token.</returns>
    Task<TokenDto?> CreateEmailConfirmationTokenAsync(string email);

    /// <summary>
    /// Generates a password reset token.
    /// </summary>
    /// <param name="email">Email for the user resetting password.</param>
    /// <returns><see cref="TokenDto"/>> object containing user information and token.</returns>
    Task<TokenDto?> CreatePasswordResetTokenAsync(string email);

    /// <summary>
    /// Checks the provided confirmation token and confirms the user.
    /// </summary>
    /// <param name="userId">Id for the unconfirmed user.</param>
    /// <param name="token">Token for the user confirmation.</param>
    /// <returns><see cref="IdentityResult"/> indicating the result of the confirmation.</returns>
    Task<IdentityResult> ConfirmUserAsync(string userid, string token);

    /// <summary>
    /// Gets the user information using userId.
    /// </summary>
    /// <param name="usedId">Unique identifier for the user.</param>
    /// <returns><see cref="UserDto"/> with user information.</returns>
    Task<UserDto?> GetUserByIdAsync(string usedId);

    /// <summary>
    /// Logs in an existing user and generates an authentication token.
    /// </summary>
    /// <param name="model">The login details (e.g., email and password) provided by the client.</param>
    /// <returns>Token containing the result of the login process.</returns>
    Task<AuthTokenDto?> LoginAsync(LoginModel model);

    /// <summary>
    /// Validates the invite code to determine if it's valid or not.
    /// This is a sample validation, which can be extended to check against a database or other validation sources.
    /// </summary>
    /// <param name="inviteCode">The invite code to validate.</param>
    /// <returns>True if the invite code is valid, otherwise false.</returns>
    public bool IsValidInviteCode(string inviteCode);
}