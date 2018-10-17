using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiFiscal.Models
{
    //http://www.afip.gob.ar/fe/documentos/manual_desarrollador_COMPG_v2_12.pdf
    public static class AfipCriarNotaFistal
    {

        public static string GetXml(Envelope objXml)
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
            "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"> " +
                   "<soap:Body>" +
                   "<FECAESolicitar xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                       "<Auth>" +
                           "<Token>" + objXml.Body.FECAESolicitar.Auth.Token + "</Token>" +
                           "<Sign>" + objXml.Body.FECAESolicitar.Auth.Sign + "</Sign>" +
                           "<Cuit>" + objXml.Body.FECAESolicitar.Auth.Cuit + "</Cuit>" +
                       "</Auth>" +
                       "<FeCAEReq>" +
                           "<FeCabReq>" +
                               "<CantReg>" + objXml.Body.FECAESolicitar.FeCAEReq.FeCabReq.CantReg + "</CantReg>" +
                               "<PtoVta>" + objXml.Body.FECAESolicitar.FeCAEReq.FeCabReq.PtoVta + "</PtoVta>" +
                               "<CbteTipo>" + objXml.Body.FECAESolicitar.FeCAEReq.FeCabReq.CbteTipo + "</CbteTipo>" +
                           "</FeCabReq>" +
                       "<FeDetReq>" +
                           "<FECAEDetRequest>" +
                               "<Concepto>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Concepto + "</Concepto>" +
                               "<DocTipo>" + (int)objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.DocTipo + "</DocTipo>" +
                               "<DocNro>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.DocNro + "</DocNro>" +
                               "<CbteDesde>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbteDesde + "</CbteDesde>" +
                               "<CbteHasta>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbteHasta + "</CbteHasta>" +
                                "<CbteFch>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbteFch.ToString("yyyMMdd") + "</CbteFch>" +
                                "<ImpTotal>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpTotal.ToString().Replace(",", ".") + "</ImpTotal>" +
                                "<ImpTotConc>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpTotConc + "</ImpTotConc>" +
                                "<ImpNeto>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpNeto.ToString().Replace(",", ".") + "</ImpNeto>" +
                                "<ImpOpEx>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpOpEx + "</ImpOpEx>" +
                                "<ImpTrib>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpTrib + "</ImpTrib>" +
                                "<ImpIVA>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.ImpIVA.ToString().Replace(",", ".") + "</ImpIVA>" +
                                "<FchServDesde>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.FchServDesde.ToString("yyyMMdd") + "</FchServDesde>" +
                                "<FchServHasta>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.FchServHasta.ToString("yyyMMdd") + "</FchServHasta>" +
                                "<FchVtoPago>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.FchVtoPago.ToString("yyyMMdd") + "</FchVtoPago>";
                                xml += "<MonId>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.MonId + "</MonId>" +
                                "<MonCotiz>" + objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.MonCotiz + "</MonCotiz>";
                                xml += objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbtesAsoc.CbteAsoc.Count > 0 ? "<CbtesAsoc>" : "";
                                foreach (var c in objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbtesAsoc.CbteAsoc)
                                {
                                    xml += "<CbteAsoc>" +
                                                "<Tipo>" + c.Tipo + "</Tipo>" +
                                                "<PtoVta>" + c.PtoVta + "</PtoVta>" +
                                                "<Nro>" + c.Nro + "</Nro>" +
                                            "</CbteAsoc>";
                                }
                                xml += objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.CbtesAsoc.CbteAsoc.Count > 0 ? "</CbtesAsoc>" : "";
                                xml += "<Tributos>";
                                foreach (var t in objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Tributos.Tributo)
                                {
                                    xml += "<Tributo>" +
                                               "<Id>" + t.Id + "</Id>";
                                    xml += t.Desc != null ? "<Desc>" + t.Desc + "</Desc>" : null;
                                    xml += "<BaseImp>" + t.BaseImp.ToString().Replace(",",".") + "</BaseImp>" +
                                    "<Alic>" + t.Alic + "</Alic>" +
                                    "<Importe>" + t.Importe.ToString().Replace(",", ".") + "</Importe>" +
                                "</Tributo>";
                                }
                                xml += "</Tributos>" +
                                 "<Iva>";
                                foreach (var i in objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Iva.AlicIva)
                                {
                                    xml += "<AlicIva>" +
                                                "<Id>" + i.Id + "</Id>" +
                                                "<BaseImp>" + i.BaseImp.ToString().Replace(",", ".") + "</BaseImp>" +
                                                "<Importe>" + i.Importe.ToString().Replace(",", ".") + "</Importe>" +
                                            "</AlicIva>";
                                }
                                xml += "</Iva>";
                                xml += objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Opcionales.Opcional.Count > 0 ? "<Opcionales>" : "";
                                foreach (var o in objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Opcionales.Opcional)
                                {
                                    xml += "<Opcional>" +
                                                "<Id>" + o.Id + "</Id>" +
                                                "<Valor>" + o.Valor + "</Valor>" +
                                            "</Opcional>";
                                }
                                xml += objXml.Body.FECAESolicitar.FeCAEReq.FeDetReq.FECAEDetRequest.Opcionales.Opcional.Count > 0 ? "</Opcionales>" : "";
                             xml += "</FECAEDetRequest>" +
                         "</FeDetReq>" +
                         "</FeCAEReq>" +
                     "</FECAESolicitar>" +
                     "</soap:Body>" +
                     "</soap:Envelope>";

            return xml;
        }

        public static string GetXmlAuth(string token, string sign, long cuit, string type)
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                      "<soap:Body>" +
                        "<" + type + " xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                          "<Auth>" +
                            "<Token>" + token + "</Token>" +
                            "<Sign>" + sign + "</Sign>" +
                            "<Cuit>" + cuit + "</Cuit>" +
                          "</Auth>" +
                        "</" + type + ">" +
                      "</soap:Body>" +
                    "</soap:Envelope>";
            return xml;
        }
    }

    public class Auth
    {
        protected Auth()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Token retornado pela WSAA</param>
        /// <param name="sign">Sinal retornado pela WSAA</param>
        /// <param name="cuit">Contribuinte Cuit (representado ou Emitente)</param>
        public Auth(string token, string sign, long cuit)
        {
            Token = token;
            Sign = sign;
            Cuit = cuit;
        }
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
    }

    public class FeCabReq
    {
        protected FeCabReq()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cantReg">Quantidade de registros dos detalhes do voucher ou lote de vouchers</param>
        /// <param name="ptoVta">Ponto de Venda do voucher que está sendo reportado. Se mais de um recibo for informado, todos devem corresponder ao mesmo ponto de venda. Obs: è oq esta na tabela PONTOS_DE_VENDA</param>
        /// <param name="cbteTipo">Tipo de voucher que está sendo relatado. Se mais de um recibo for informado, todos devem ser do mesmo tipo. OBS: Com Factura A só pode ser usado o documento CUIT(CNPJ), campo RG no EVO. Com factura B somente DNI(CPF)</param>
        public FeCabReq(int cantReg, int ptoVta, int cbteTipo)
        {
            CantReg = cantReg;
            PtoVta = ptoVta;
            CbteTipo = cbteTipo;
        }
        public int CantReg { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
    }

    public class CbteAsoc
    {
        protected CbteAsoc()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo">Código do tipo de voucher. Verifique o método FEParamGetTiposCbte</param>
        /// <param name="ptoVta">Ponto de venda</param>
        /// <param name="nro">Numero do comprovante</param>
        public CbteAsoc(short tipo, int ptoVta, long nro)
        {
            Tipo = tipo;
            PtoVta = ptoVta;
            Nro = nro;
        }
        public short Tipo { get; set; }
        public int PtoVta { get; set; }
        public long Nro { get; set; }
    }

    public class CbtesAsoc
    {
        public CbtesAsoc()
        {
            CbteAsoc = new List<CbteAsoc>();
        }
        public CbtesAsoc(List<CbteAsoc> cbteAsoc)
        {
            CbteAsoc = cbteAsoc;
        }
        public List<CbteAsoc> CbteAsoc { get; set; }
    }

    public class Tributo
    {
        protected Tributo()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Código de imposto de acordo com o método FEParamGetTiposTributos</param>
        /// <param name="baseImp">Base tributária para a determinação do imposto</param>
        /// <param name="alic">Alíquota</param>
        /// <param name="importe">Tributaçao de importações</param>
        /// <param name="desc">Descrição do tributo.</param>
        public Tributo(short id, double baseImp, double alic, double importe, string desc = null)
        {
            Id = id;
            BaseImp = baseImp;
            Alic = alic;
            Importe = importe;
            Desc = desc;
        }
        public short Id { get; set; }
        public string Desc { get; set; }
        public double BaseImp { get; set; }
        public double Alic { get; set; }
        public double Importe { get; set; }
    }

    public class Tributos
    {
        public Tributos()
        {
            Tributo = new List<Tributo>();
        }
        public Tributos(List<Tributo> tributos)
        {
            Tributo = tributos;
        }
        public List<Tributo> Tributo { get; set; }
    }

    public class AlicIva
    {
        protected AlicIva()
        {

        }
        /// <summary>
        /// Os valores informados no AlicIVA devem corresponder de acordo com o tipo de IVA selecionado. Para vouchers tipo 2, 3, 7, 8, 52 e 53 essa validação não é levada em consideração. Margem de erro: erro relativo percentual deve ser 
        /// meno ou igual a 0,01% ou erro absoluto menor ou igual a 0,01 Não aplicável para recibos tipo C
        /// </summary>
        /// <param name="id">Código de tipo de iva. Consultar método FEParamGetTiposIva</param>
        /// <param name="baseImp">Base tributária para a determinação da alíquota.</param>
        /// <param name="importe">Importaçao</param>
        public AlicIva(int id, double baseImp, double importe)
        {
            Id = id;
            BaseImp = new []{2, 3, 7, 8, 52, 53}.Contains(id) ? 0.0 : baseImp;
            Importe = new[] { 2, 3, 7, 8, 52, 53 }.Contains(id) ? 0.0 : importe;
        }
        public int Id { get; set; }
        public double BaseImp { get; set; }
        public double Importe { get; set; }
    }

    public class Iva
    {
        public Iva()
        {
            AlicIva = new List<AlicIva>();
        }
        public Iva(List<AlicIva> alicIvas)
        {
            AlicIva = alicIvas;
        }
        public List<AlicIva> AlicIva { get; set; }
    }

    public class Opcional
    {
        protected Opcional()
        {

        }
        public Opcional(string id = null, string valor = null)
        {
            Id = id;
            Valor = valor;
        }
        public string Id { get; set; }
        public string Valor { get; set; }
    }

    public class Opcionales
    {
        public Opcionales()
        {
            Opcional = new List<Opcional>();
        }

        public Opcionales(List<Opcional> opcionals)
        {
            Opcional = opcionals;
        }
        public List<Opcional> Opcional { get; set; }
    }

    public class FECAEDetRequest
    {
        protected FECAEDetRequest()
        {

        }
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
        /// <param name="impIVA">Soma das quantidades da matriz de IVA. Para vouchers tipo C, deve ser igual a zero (0). Para vouchers de Bens Usados ​​- Emitente Monotributista não deve ser informado ou deve ser igual a zero (0).</param>
        /// <param name="impTrib">Soma dos valores da matriz de tributo</param>
        /// <param name="monId">Código da moeda do vale. Verifique o método FEParamGetTiposMonedas para possíveis valores</param>
        /// <param name="monCotiz">Citação da moeda relatada. Para PES, pesos argentinos o mesmo deve ser 1</param>
        /// <param name="cbtesAsoc"></param>
        /// <param name="tributos"></param>
        /// <param name="iva"></param>
        /// <param name="opcionales"></param>
        /// <param name="cbteFch">Data do comprovante (yyyyMMdd). Para conceito igual a 1, a data de emissão do comprovante pode ser de até 5 dias antes ou depois da data de geração: Não pode exceder o mês de apresentação, se for indicado Conceito igual a 2 ou 3 pode ser de até 10 dias antes ou depois da data de geração. Se a data do voucher não for enviada, a data do processo será atribuída</param>
        /// <param name="fchServDesde"> Data de início da assinatura do serviço a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formato yyyymmdd</param>
        /// <param name="fchServHasta">Data final da assinatura do serviço a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formatar yyyymmdd. FchServUp não pode ser menor que fchServDesde</param>
        /// <param name="fchVtoPago">Serviço de data de vencimento do pagamento a ser faturado. Dados obrigatórios para o conceito 2 ou 3 (Serviços / Produtos e Serviços). Formatar yyyymmdd. Deve ser o mesmo ou mais tarde que a data do voucher.</param>
        public FECAEDetRequest(int concepto, EDocTipo docTipo, long docNro, long cbteDesde, long cbteHasta, double impTotConc, double impOpEx, double impIVA,
            string monId, double monCotiz, CbtesAsoc cbtesAsoc = null, Tributos tributos = null, Iva iva = null, Opcionales opcionales = null, DateTime? cbteFch = null, DateTime? fchServDesde = null, DateTime? fchServHasta = null, DateTime? fchVtoPago = null)
        {
            Concepto = concepto;
            DocTipo = docTipo;
            DocNro = docNro;
            CbteDesde = cbteDesde;
            CbteHasta = cbteHasta;
            ImpTotConc = impTotConc;
            ImpOpEx = impOpEx;
            ImpIVA = impIVA;
            MonId = monId;
            MonCotiz = monCotiz;
            CbtesAsoc = cbtesAsoc ?? new CbtesAsoc();
            Iva = iva ?? new Iva();
            ImpNeto = iva.AlicIva.Sum(p=> p.BaseImp);
            Opcionales = opcionales ?? new Opcionales();
            CbteFch = cbteFch ?? DateTime.Now;
            FchServDesde = fchServDesde ?? DateTime.Now;
            FchServHasta = fchServHasta ?? DateTime.Now;
            FchVtoPago = fchVtoPago ?? DateTime.Now;
            Tributos = tributos ?? new Tributos();
            ImpTrib = Tributos.Tributo.Sum(p => p.Importe);
            ImpTotal = ImpTotConc + ImpNeto + ImpOpEx + ImpTrib + ImpIVA;
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
        public double ImpIVA { get; set; }
        public DateTime FchServDesde { get; set; }
        public DateTime FchServHasta { get; set; }
        public DateTime FchVtoPago { get; set; }
        public string MonId { get; set; }
        public double MonCotiz { get; set; }
        public CbtesAsoc CbtesAsoc { get; set; }
        public Tributos Tributos { get; set; }
        public Iva Iva { get; set; }
        public Opcionales Opcionales { get; set; }
    }

    public class FeDetReq
    {
        public FECAEDetRequest FECAEDetRequest { get; set; }
    }

    public class FeCAEReq
    {
        public FeCabReq FeCabReq { get; set; }
        public FeDetReq FeDetReq { get; set; }
    }

    public class FECAESolicitar
    {
        public Auth Auth { get; set; }
        public FeCAEReq FeCAEReq { get; set; }
    }

    public class Body
    {
        public FECAESolicitar FECAESolicitar { get; set; }
    }

    public class Envelope
    {
        protected Envelope()
        {

        }
        public Envelope(Auth auth, FeCabReq feCabReq, FECAEDetRequest fECAEDetRequest)
        {
            Body = new Body
            {
                FECAESolicitar = new FECAESolicitar
                {
                    Auth = auth,
                    FeCAEReq = new FeCAEReq
                    {
                        FeCabReq = feCabReq,
                        FeDetReq = new FeDetReq
                        {
                            FECAEDetRequest = fECAEDetRequest,
                        }
                    }
                }
            };
            Header = "";
        }
        public string Header { get; set; }
        public Body Body { get; set; }
    }

    //login
    public class Header
    {
        public string source { get; set; }
        public string destination { get; set; }
        public long uniqueId { get; set; }
        public DateTime generationTime { get; set; }
        public DateTime expirationTime { get; set; }
    }

    public class Credentials
    {
        public string token { get; set; }
        public string sign { get; set; }
    }

    public class loginTicketResponse
    {
        public Header header { get; set; }
        public Credentials credentials { get; set; }
        public string Version { get; set; }
    }

    //enum
    public enum EDocTipo
    {
        CUIT = 80,
        CUIL = 86,
        CDI = 87
    }
}