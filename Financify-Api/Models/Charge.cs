using Financify_Api.Models.Enums;

namespace Financify_Api.Models
{
    public class Charge
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? DueDate { get; set; }

        public string? Value { get; set; }

        public ChargeStatus Status { get; set; }
    }
}
