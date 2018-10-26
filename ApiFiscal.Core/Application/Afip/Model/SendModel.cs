using ApiFiscal.Core.Domain.Afip.Entity;
using ApiFiscal.Core.Domain.Afip.Enum;

namespace ApiFiscal.Core.Application.Afip.ModelreceiveParameters
{
    public class SendModel
    {
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
        public int IdIva { get; set; }
        public double Amount { get; set; }
        public double Iva { get; set; }
        public int Concepto { get; set; }
        public EDocTipo DocTipo { get; set; }
        public long DocNro { get; set; }
        public int CantReg { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
    }
}
