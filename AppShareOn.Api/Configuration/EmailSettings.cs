/// <summary>
/// Represents the configuration settings required to send emails via SMTP.
/// This class holds the settings for SMTP server, port, authentication, and security.
/// </summary>
public class EmailSettings
{
    /// <summary>
    /// Gets or sets the SMTP server address.
    /// This is the address of the email server (e.g., smtp.gmail.com, smtp.mailtrap.io).
    /// </summary>
    public string SmtpServer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the port number for the SMTP server.
    /// Common SMTP ports are 25, 587 (TLS), or 465 (SSL).
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username for authentication with the SMTP server.
    /// Typically, this will be your email address or account identifier.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for authenticating with the SMTP server.
    /// This should be kept secure and ideally not hardcoded in the source code.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether SSL (Secure Socket Layer) is enabled for the SMTP connection.
    /// If true, the connection will be encrypted for secure transmission of emails.
    /// </summary>
    public bool EnableSsl { get; set; }
}