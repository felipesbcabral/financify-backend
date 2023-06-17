using Financify_Api.Models;
using MimeKit;

namespace Financify_Api.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(EmailMessage message);
    }
}
