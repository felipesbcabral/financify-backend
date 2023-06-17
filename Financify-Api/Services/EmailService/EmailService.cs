using Financify_Api.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Financify_Api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig) => _emailConfig = emailConfig;

        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("email", _emailConfig.From));
            email.To.AddRange(message.To);
            email.Subject = message.Subject;
            email.Body = new TextPart(TextFormat.Text) { Text = message.Content };

            return email;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
