using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using AppShareOn.Application.Dtos;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Models;
using AppShareOn.Application.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AppShareOn.Application.Services;

/// <summary>
/// Service for handling user authentication operations such as registration, login, and token generation.
/// Implements the <see cref="IAuthService"/> interface.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{IdentityUser}"/> service for managing users.</param>
    /// <param name="signInManager">The <see cref="SignInManager{IdentityUser}"/> service for user sign-in functionality.</param>
    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        IEmailService emailService,
        IHttpClientFactory httpClientFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), $"{nameof(userManager)} cannot be null.");
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager), $"{nameof(signInManager)} cannot be null.");
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} cannot be null.");
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService), $"{nameof(emailService)} cannot be null.");
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory), $"{nameof(httpClientFactory)} cannot be null.");
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<object>> RequestPasswordResetAsync(string email)
    {
        // Generate password reset token.
        var tokenDto = await CreatePasswordResetTokenAsync(email);

        // Check if token generation fails and respond with error.
        if (tokenDto == null)
            return ApiResponse<object>.ErrorResponse(["Failed to process password reset for the provided user."], "Password reset failed.");

        // Prepare and send email.
        var subject = "Reset Your Password";
        var resetLink = GeneratePasswordResetLink(tokenDto.Token);
        var body = $@"
        <html>
        <body>
            <p>Dear User,</p>
            <p>You have requested to reset your password. Please reset your password by clicking the link below:</p>
            <p><a href='{resetLink}'>Reset your password</a></p>
            <p>If you cannot click the link, please copy and paste the following URL into your browser:</p>
            <p>{resetLink}</p>
            <p>Best regards,<br>Your Company</p>
        </body>
        </html>";

        try
        {
            await _emailService.SendEmailAsync(email, subject, body);
        }
        catch (Exception ex)
        {
            return ApiResponse<object>.ErrorResponse(["Failed to send password reset email. Please try again or contact support.", ex.Message], "Failed to send password reset email.");
        }

        // Registration complete. Send success data.
        return ApiResponse<object>.SuccessResponse(new { }, "Password reset succeeded. Recovery instructions were sent to your email.");
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<object>> ProcessPasswordResetAsync(PasswordResetModel model, string token)
    {
        // Get the user with the given email.
        var user = await _userManager.FindByEmailAsync(model.Email);

        // Respond with error if user not found.
        if (user == null)
            return ApiResponse<object>.ErrorResponse(["Failed to process password reset for the provided user."], "Password reset failed.");

        // Reset user password.
        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

        // Respond with the identity provided errors if password reset not successful.
        if (!result.Succeeded)
            return ApiResponse<object>.ErrorResponse(result.Errors.Select(x => x.Description), "Password reset failed.");

        // Password reset success.
        return ApiResponse<object>.SuccessResponse(new {}, "Password reset succeeded. Please continue to login using your new credentials.");
    }

    /// <inheritdoc/>
    public async Task<ApiResponse<string>> RegisterAsync(RegisterModel model)
    {
        // Check if the invite code is valid.
        if (!IsValidInviteCode(model.InviteCode))
            return ApiResponse<string>.ErrorResponse(["Invalid invite code"], "User registration failed.");

        // Call the AuthService to handle registration logic.
        var result = await CreateUserAsync(model);

        // Check if successful and respond with errors if not.
        if (!result.Succeeded)
            return ApiResponse<string>.ErrorResponse(result.Errors.Select(x => x.Description), "User registration failed.");

        // If registration is successful then generate confirmation token and send it to user.
        var tokenDto = await CreateEmailConfirmationTokenAsync(model.Email);

        // Check if token generation fails and respond with error.
        if (tokenDto == null)
            return ApiResponse<string>.ErrorResponse(["Email not found."], "Failed to generate email confirmation link.");

        // Prepare and send email.
        var subject = "Please Confirm Your Email Address";
        var confirmationLink = GenerateEmailConfirmationLink(tokenDto.UserId, tokenDto.Token);
        var body = $@"
            <html>
            <body>
                <p>Dear {model.FirstName} {model.LastName},</p>
                <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
                <p><a href='{confirmationLink}'>Confirm your email</a></p>
                <p>If you cannot click the link, please copy and paste the following URL into your browser:</p>
                <p>{confirmationLink}</p>
                <p>Best regards,<br>Your Company</p>
            </body>
            </html>";

        try
        {
            await _emailService.SendEmailAsync(model.Email, subject, body);
        }
        catch (Exception)
        {
            return ApiResponse<string>.ErrorResponse(["Failed to send confirmation email. Please try again or contact support."], "Failed to send confirmation email.");
        }

        // Registration complete. Send success data.
        return ApiResponse<string>.SuccessResponse(tokenDto.UserId, "User is registered successfully. Please confirm email.");
    }

    /// <inheritdoc/>
    public async Task<TokenDto?> CreateEmailConfirmationTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        return new TokenDto { UserId = user.Id, Token = await _userManager.GenerateEmailConfirmationTokenAsync(user) };
    }

    /// <inheritdoc/>
    public async Task<TokenDto?> CreatePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        return new TokenDto { UserId = user.Id, Token = await _userManager.GeneratePasswordResetTokenAsync(user) };
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> ConfirmUserAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        return IdentityResult.Failed();
    }

    /// <inheritdoc/>
    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
            return new UserDto { UserName = user.UserName ?? "", Email = user.Email ?? "" };

        return null;
    }

    /// <inheritdoc/>
    public bool IsValidInviteCode(string inviteCode)
    {
        // Simulate invite code validation logic. Can be replaced with real database lookup or business logic.
        return inviteCode == "VALID_CODE";
    }

    /// <inheritdoc/>
    public async Task<AuthTokenDto?> LoginAsync(LoginModel model)
    {
        // Check if the user exists with the provided email
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return null;

        // Attempt to sign in the user with the provided password
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!result.Succeeded)
            return null;

        // Setup claims.
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var expiresAt = DateTime.UtcNow.AddMinutes(10);

        return new AuthTokenDto
        {
            ExpiresAt = expiresAt,
            Token = CreateToken(claims, expiresAt)
        };
    }

    /// <summary>
    /// Helper method to create JWT Token with list of claims and expiration date..
    /// </summary>
    /// <param name="claims">List of provided claims</param>
    /// <param name="expiresAt">Expiration date-time.</param>
    /// <returns></returns>
    private string CreateToken(List<Claim> claims, DateTime expiresAt)
    {
        // Get jwt settings from configuration.
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "");

        // Setup JWT.
        var jwt = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="model">The registration details provided by the client.</param>
    /// <returns>An <see cref="IdentityResult"/> containing the result of the registration process.</returns>
    private async Task<IdentityResult> CreateUserAsync(RegisterModel model)
    {
        // Create a new user from the provided registration model
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        // Attempt to create the user with the provided password
        var result = await _userManager.CreateAsync(user, model.Password);

        return result;
    }

    /// <summary>
    /// Generates an email confirmation link.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>    
    private string GenerateEmailConfirmationLink(string userId, string token)
    {
        var client = _httpClientFactory.CreateClient("AppshareonClient");

        return $"{client.BaseAddress}account/{userId}/confirm/{HttpUtility.UrlEncode(token)}";
    }

    /// <summary>
    /// Generates a password reset link.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private string GeneratePasswordResetLink(string token)
    {
        var client = _httpClientFactory.CreateClient("AppshareonClient");
        return $"{client.BaseAddress}account/reset-password/{HttpUtility.UrlEncode(token)}";
    }
}