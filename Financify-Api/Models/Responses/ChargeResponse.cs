using Financify_Api.Models.Enums;

namespace Financify_Api.Models.Responses
{
    public class ChargeResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Value { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid AccountId { get; set; }
    }
}
