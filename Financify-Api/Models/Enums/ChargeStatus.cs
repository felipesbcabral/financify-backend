using System.ComponentModel;

namespace Financify_Api.Models.Enums
{
    public enum ChargeStatus
    {
        [Description("Payed")]
        Payed = 1,
        [Description("Expired")]
        Late = 2,
        [Description("Pending")]
        Pending = 3,
    }
}
