
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace RepositoryPatternWithUOW.EfCore.MailService
{
    public class MailService : ISenderService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string emailTo, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(emailTo));
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.Body = bodyBuilder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.Name,_mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
