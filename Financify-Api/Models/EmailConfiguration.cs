namespace Financify_Api.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public string SmtpServer { get; set; } = string.Empty;

        public int Port { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
