using System.ComponentModel;

namespace Financify_Api.Models.Enums
{
    public enum ChargeStatus
    {
        [Description("Already payed")]
        Payed = 1,
        [Description("Passed the limit date")]
        Late = 2,
        [Description("Pending to pay")]
        Pending = 3,
    }
}
