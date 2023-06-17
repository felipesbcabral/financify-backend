namespace Financify_Api.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? ResetToken { get; set; } = string.Empty;

        public DateTime? ResetTokenExpires { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
