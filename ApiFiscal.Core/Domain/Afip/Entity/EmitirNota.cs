using System.Globalization;

namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class EmitirNota: BaseEntity
    {
        public EmitirNota(Auth auth, FeCabReq feCabReq, FecaeDetRequest fecaeDetRequest)
        {
            Auth = auth;
            FeCabReq = feCabReq;
            FecaeDetRequest = fecaeDetRequest;
        }

        protected override void ValidateOnCreate()
        {
            if(Auth?.IsValid != true || FeCabReq?.IsValid != true || FecaeDetRequest?.IsValid != true)
            {
                IsValid = false;
            }
        }

        public Auth Auth { get; set; }
        public FeCabReq FeCabReq { get; set; }
        public FecaeDetRequest FecaeDetRequest { get; set; }

        public string GetXmlString()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
            "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"> " +
                   "<soap:Body>" +
                   "<FECAESolicitar xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                       "<Auth>" +
                           "<Token>" + Auth.Token + "</Token>" +
                           "<Sign>" + Auth.Sign + "</Sign>" +
                           "<Cuit>" + Auth.Cuit + "</Cuit>" +
                       "</Auth>" +
                       "<FeCAEReq>" +
                           "<FeCabReq>" +
                               "<CantReg>" + FeCabReq.CantReg + "</CantReg>" +
                               "<PtoVta>" + FeCabReq.PtoVta + "</PtoVta>" +
                               "<CbteTipo>" + FeCabReq.CbteTipo + "</CbteTipo>" +
                           "</FeCabReq>" +
                       "<FeDetReq>" +
                           "<FECAEDetRequest>" +
                               "<Concepto>" + FecaeDetRequest.Concepto + "</Concepto>" +
                               "<DocTipo>" + (int)FecaeDetRequest.DocTipo + "</DocTipo>" +
                               "<DocNro>" + FecaeDetRequest.DocNro + "</DocNro>" +
                               "<CbteDesde>" + FecaeDetRequest.CbteDesde + "</CbteDesde>" +
                               "<CbteHasta>" + FecaeDetRequest.CbteHasta + "</CbteHasta>" +
                                "<CbteFch>" + FecaeDetRequest.CbteFch.ToString("yyyMMdd") + "</CbteFch>" +
                                "<ImpTotal>" + FecaeDetRequest.ImpTotal.ToString(CultureInfo.InvariantCulture) + "</ImpTotal>" +
                                "<ImpTotConc>" + FecaeDetRequest.ImpTotConc + "</ImpTotConc>" +
                                "<ImpNeto>" + FecaeDetRequest.ImpNeto.ToString(CultureInfo.InvariantCulture) + "</ImpNeto>" +
                                "<ImpOpEx>" + FecaeDetRequest.ImpOpEx + "</ImpOpEx>" +
                                "<ImpTrib>" + FecaeDetRequest.ImpTrib + "</ImpTrib>" +
                                "<ImpIVA>" + FecaeDetRequest.ImpIva.ToString(CultureInfo.InvariantCulture) + "</ImpIVA>" +
                                "<FchServDesde>" + FecaeDetRequest.FchServDesde.ToString("yyyMMdd") + "</FchServDesde>" +
                                "<FchServHasta>" + FecaeDetRequest.FchServHasta.ToString("yyyMMdd") + "</FchServHasta>" +
                                "<FchVtoPago>" + FecaeDetRequest.FchVtoPago.ToString("yyyMMdd") + "</FchVtoPago>";
                                xml += "<MonId>" + FecaeDetRequest.MonId + "</MonId>" +
                                "<MonCotiz>" + FecaeDetRequest.MonCotiz + "</MonCotiz>";
                                xml += FecaeDetRequest.CbtesAsoc.Count > 0 ? "<CbtesAsoc>" : "";
                                foreach (var c in FecaeDetRequest.CbtesAsoc)
                                {
                                    xml += "<CbteAsoc>" +
                                                "<Tipo>" + c.Tipo + "</Tipo>" +
                                                "<PtoVta>" + c.PtoVta + "</PtoVta>" +
                                                "<Nro>" + c.Nro + "</Nro>" +
                                            "</CbteAsoc>";
                                }
                                xml += FecaeDetRequest.CbtesAsoc.Count > 0 ? "</CbtesAsoc>" : "";
                                xml += FecaeDetRequest.Tributos.Count > 0 ? "<Tributos>" : "";
                                foreach (var t in FecaeDetRequest.Tributos)
                                {
                                    xml += "<Tributo>" +
                                               "<Id>" + t.Id + "</Id>";
                                    xml += t.Desc != null ? "<Desc>" + t.Desc + "</Desc>" : null;
                                    xml += "<BaseImp>" + t.BaseImp.ToString(CultureInfo.InvariantCulture) + "</BaseImp>" +
                                    "<Alic>" + t.Alic + "</Alic>" +
                                    "<Importe>" + t.Importe.ToString(CultureInfo.InvariantCulture) + "</Importe>" +
                                "</Tributo>";
                                }
                                xml += FecaeDetRequest.Tributos.Count > 0 ? "</Tributos>" : "";
                                xml += "<Iva>";
                                foreach (var i in FecaeDetRequest.Ivas)
                                {
                                    xml += "<AlicIva>" +
                                                "<Id>" + i.Id + "</Id>" +
                                                "<BaseImp>" + i.BaseImp.ToString(CultureInfo.InvariantCulture) + "</BaseImp>" +
                                                "<Importe>" + i.Importe.ToString(CultureInfo.InvariantCulture) + "</Importe>" +
                                            "</AlicIva>";
                                }
                                xml += "</Iva>";
                                xml += FecaeDetRequest.Opcionales.Count > 0 ? "<Opcionales>" : "";
                                foreach (var o in FecaeDetRequest.Opcionales)
                                {
                                    xml += "<Opcional>" +
                                                "<Id>" + o.Id + "</Id>" +
                                                "<Valor>" + o.Valor + "</Valor>" +
                                            "</Opcional>";
                                }
                                xml += FecaeDetRequest.Opcionales.Count > 0 ? "</Opcionales>" : "";
                                xml += "</FECAEDetRequest>" +
                            "</FeDetReq>" +
                            "</FeCAEReq>" +
                        "</FECAESolicitar>" +
                        "</soap:Body>" +
                        "</soap:Envelope>";

            return xml;
        }
    }
}