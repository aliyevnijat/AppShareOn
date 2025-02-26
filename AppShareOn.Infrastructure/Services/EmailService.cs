using System.Net.Mail;
using AppShareOn.Application.Interfaces;

/// <summary>
/// Concrete implementation of the <see cref="IEmailService"/> interface.
/// Uses an SMTP client to send emails.
/// </summary>
public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="smtpClient">An instance of <see cref="SmtpClient"/> to handle sending emails.</param>
    public EmailService(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient), "SMTP client cannot be null.");
    }

    /// <inheritdoc/>
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        if (string.IsNullOrEmpty(to))
        {
            throw new ArgumentException("Recipient email address cannot be null or empty.", nameof(to));
        }

        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentException("Email subject cannot be null or empty.", nameof(subject));
        }

        if (string.IsNullOrEmpty(body))
        {
            throw new ArgumentException("Email body cannot be null or empty.", nameof(body));
        }

        var mailMessage = new MailMessage("from@example.com", to, subject, body);

        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Handle potential exceptions (e.g., network issues, SMTP server errors)
            throw new InvalidOperationException("Failed to send email.", ex);
        }
    }
}