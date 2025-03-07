using EasyLoginBase.Application.Dto.Email;
using EasyLoginBase.Services.Tools.Email;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace EasyLoginBase.Services.Services.Email;

public interface IEmailService
{
    Task<bool> EnviarEmailAsync(EmailDto emailDto);
}
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> EnviarEmailAsync(EmailDto emailDto)
    {
        try
        {
            using (SmtpClient smtp = new())
            {
                smtp.Host = _emailSettings.SmtpServer;
                smtp.Port = _emailSettings.Port;
                smtp.EnableSsl = _emailSettings.EnableSsl;
                smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                using (MailMessage msg = new())
                {
                    msg.From = new MailAddress(_emailSettings.Username);
                    msg.To.Add(new MailAddress(emailDto.EmailDestinatario));
                    msg.Subject = emailDto.Assunto;
                    msg.Body = emailDto.Corpo;
                    msg.IsBodyHtml = true; // Permite enviar HTML no e-mail

                    await smtp.SendMailAsync(msg);
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
