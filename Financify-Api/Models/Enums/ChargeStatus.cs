using System.ComponentModel;

namespace Financify_Api.Models.Enums
{
    public enum ChargeStatus
    {
        [Description("Pago")]
        Pago = 1,
        [Description("Expirado")]
        Expirado = 2,
        [Description("Pendente")]
        Pendente = 3,
    }
}
