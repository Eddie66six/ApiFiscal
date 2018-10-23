using System;
using System.Linq;

namespace ApiFiscal.Core.Entity.Afip
{
    public sealed class AlicIva
    {
        /// <summary>
        /// Os valores informados no AlicIVA devem corresponder de acordo com o tipo de IVA selecionado. Para vouchers tipo 2, 3, 7, 8, 52 e 53 essa validação não é levada em consideração. Margem de erro: erro relativo percentual deve ser 
        /// meno ou igual a 0,01% ou erro absoluto menor ou igual a 0,01 Não aplicável para recibos tipo C
        /// </summary>
        /// <param name="id">Código de tipo de iva. Consultar método FEParamGetTiposIva</param>
        /// <param name="amount">Total de produtos</param>
        /// <param name="iva">porcentagem do iva</param>
        /// <param name="included">Se o valor total ja esta incluso a porcentagem do iva</param>
        public static AlicIva Get(int id, double amount, double iva, bool included = true)
        {
            return new AlicIva(id, amount, iva, included);
        }
        private AlicIva(int id, double amount, double iva, bool included)
        {
            Id = id;
            var arrayTmp = new[] {2, 3, 7, 8, 52, 53};
            if (included)
            {
                BaseImp = arrayTmp.Contains(id) ? 0.0 : Math.Round(amount / ((iva / 100) + 1), 2);
                Importe = arrayTmp.Contains(id) ? 0.0 : Math.Round(amount - BaseImp, 2);
            }
            else
            {
                BaseImp = arrayTmp.Contains(id) ? 0.0 : Math.Round((amount * iva) / 100, 2);
                Importe = arrayTmp.Contains(id) ? 0.0 : Math.Round(amount + BaseImp, 2);
            }
        }
        public int Id { get; set; }
        public double BaseImp { get; set; }
        public double Importe { get; set; }
    }
}