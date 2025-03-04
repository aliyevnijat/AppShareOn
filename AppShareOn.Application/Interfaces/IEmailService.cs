namespace AppShareOn.Application.Interfaces;

/// <summary>
/// Defines the contract for sending emails in the application.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmailAsync(string to, string subject, string body);
}