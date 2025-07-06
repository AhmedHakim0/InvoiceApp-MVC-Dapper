
namespace InvoiceWebApp.Helpers;

public class EmailSender
{
    private readonly SmtpSettings _smtp;

	public EmailSender(IOptions<SmtpSettings> options)
	{
		_smtp = options.Value;
    }

    // Sends an email using the SMTP settings provided in the configuration.
    public void SendEmail(string toEmail, string subject, string body)
    {
        var client = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            EnableSsl = _smtp.EnableSSL,
            Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password)
        };

        var message = new MailMessage(_smtp.UserName, toEmail, subject, body);
        message.IsBodyHtml = true;

        client.Send(message);
    }
}
