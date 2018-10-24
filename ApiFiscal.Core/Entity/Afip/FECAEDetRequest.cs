using System;
using System.Collections.Generic;
using System.Linq;
using ApiFiscal.Core.Enum.Afip;

namespace ApiFiscal.Core.Entity.Afip
{
    public sealed class FecaeDetRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concepto">Conceito de comprovante. Valores permitidos: 1 Produtos 2 Serviços 3 Produtos e Serviços</param>
        /// <param name="docTipo">Código do documento de identificação do comprador</param>
        /// <param name="docNro">Nº de identificação do comprador</param>
        /// <param name="cbteDesde"></param>
        /// <param name="cbteHasta"></param>
        /// <param name="impTotConc">Valor líquido não tributado. Deve ser menor ou igual a quantidade total e não pode ser menor que zero. Não pode ser maior que o Valor Total da operação ou menor que zero (0). Para vouchers tipo C, deve ser igual a zero (0). Para recibos de Mercadorias Usadas - Emissor Monotributista este campo corresponde ao valor do subtotal.</param>
        /// <param name="impOpEx">Quantidade isenta. Deve ser menor ou igual a quantidade total e não pode ser menor que zero. Para vouchers tipo C, deve ser igual a zero (0). Para vouchers de Bens Usados ​​- Emitente Monotributista não deve ser informado ou deve ser igual a zero (0).</param>
        /// <param name="impIva">Soma das quantidades da matriz de IVA. Para vouchers tipo C, deve ser igual a zero (0). Para vouchers de Bens Usados ​​- Emitente Monotributista não deve ser informado ou deve ser igual a zero (0).</param>
        /// <param name="monId">Código da moeda do vale. Verifique o método FEParamGetTiposMonedas para possíveis valores</param>
        /// <param name="monCotiz">Citação da moeda relatada. Para PES, pesos argentinos o mesmo deve ser 1</param>
        /// <param name="cbtesAsoc"></param>
        /// <param name="tributos"></param>
        /// <param name="ivas"></param>
        /// <param name="opcionales"></param>
        /// <param name="cbteFch">Data do comprovante (yyyyMMdd). Para conceito igual a 1, a data de emissão do comprovante pode ser de até 5 dias antes ou depois da data de geração: Não pode exceder o mês de apresentação, se for indicado Conceito igual a 2 ou 3 pode ser de até 10 dias antes ou depois da data de geração. Se a data do voucher não for enviada, a data do processo será atribuída</param>
        /// <param name="fchServDesde"> Data de início da assinatura do serviço a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formato yyyymmdd</param>
        /// <param name="fchServHasta">Data final da assinatura do serviço a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formatar yyyymmdd. FchServUp não pode ser menor que fchServDesde</param>
        /// <param name="fchVtoPago">Serviço de data de vencimento do pagamento a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formatar yyyymmdd. Deve ser o mesmo ou mais tarde que a data do voucher.</param>
        public static FecaeDetRequest Get(int concepto, EDocTipo docTipo, long docNro, long cbteDesde, long cbteHasta, double impTotConc, double impOpEx, double impIva,
            string monId, double monCotiz, List<CbteAsoc> cbtesAsoc = null, List<Tributo> tributos = null, List<AlicIva> ivas = null, List<Opcional> opcionales = null,
            DateTime? cbteFch = null, DateTime? fchServDesde = null, DateTime? fchServHasta = null, DateTime? fchVtoPago = null)
        {
            return new FecaeDetRequest(concepto, docTipo, docNro, cbteDesde,cbteHasta, impTotConc, impOpEx, impIva, monId, monCotiz, cbtesAsoc, tributos,ivas,opcionales,
                cbteFch, fchServDesde, fchServHasta, fchVtoPago);
        }

        private FecaeDetRequest(int concepto, EDocTipo docTipo, long docNro, long cbteDesde, long cbteHasta, double impTotConc, double impOpEx, double impIva,
            string monId, double monCotiz, List<CbteAsoc> cbtesAsoc = null, List<Tributo> tributos = null, List<AlicIva> ivas = null, List<Opcional> opcionales = null,
            DateTime? cbteFch = null, DateTime? fchServDesde = null, DateTime? fchServHasta = null, DateTime? fchVtoPago = null)
        {
            Concepto = concepto;
            DocTipo = docTipo;
            DocNro = docNro;
            CbteDesde = cbteDesde;
            CbteHasta = cbteHasta;
            ImpTotConc = impTotConc;
            ImpOpEx = impOpEx;
            ImpIva = impIva;
            MonId = monId;
            MonCotiz = monCotiz;
            CbtesAsoc = cbtesAsoc ?? new List<CbteAsoc>();
            Ivas = ivas ?? new List<AlicIva>();
            ImpNeto = Ivas.Sum(p => p.BaseImp);
            Opcionales = opcionales ?? new List<Opcional>();
            CbteFch = cbteFch ?? DateTime.Now;
            FchServDesde = fchServDesde ?? DateTime.Now;
            FchServHasta = fchServHasta ?? DateTime.Now;
            FchVtoPago = fchVtoPago ?? DateTime.Now;
            Tributos = tributos ?? new List<Tributo>();
            ImpTrib = Tributos.Sum(p => p.Importe);
            ImpTotal = ImpTotConc + ImpNeto + ImpOpEx + ImpTrib + ImpIva;
        }
        public int Concepto { get; set; }
        public EDocTipo DocTipo { get; set; }
        public long DocNro { get; set; }
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public DateTime CbteFch { get; set; }
        public double ImpTotal { get; set; }
        public double ImpTotConc { get; set; }
        public double ImpNeto { get; set; }
        public double ImpOpEx { get; set; }
        public double ImpTrib { get; set; }
        public double ImpIva { get; set; }
        public DateTime FchServDesde { get; set; }
        public DateTime FchServHasta { get; set; }
        public DateTime FchVtoPago { get; set; }
        public string MonId { get; set; }
        public double MonCotiz { get; set; }
        public List<CbteAsoc> CbtesAsoc { get; set; }
        public List<Tributo> Tributos { get; set; }
        public List<AlicIva> Ivas { get; set; }
        public List<Opcional> Opcionales { get; set; }
    }
}