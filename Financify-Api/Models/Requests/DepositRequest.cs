using System;

namespace Financify_Api.Models.Requests
{
    public class DepositRequest
    {
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }
        public string NumeroCartao { get; set; }
        public string DataValidade { get; set; }
        public string CodigoSeguranca { get; set; }
        public string Senha { get; set; }
    }
}
