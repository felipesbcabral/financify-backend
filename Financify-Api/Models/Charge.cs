using Financify_Api.Models.Enums;
using Financify_Api.Models.Enums.Extensions;
using Newtonsoft.Json;

namespace Financify_Api.Models
{
    public class Charge
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Value { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid AccountId { get; set; }

        public ChargeStatus Status { get; set; }
    }
}
