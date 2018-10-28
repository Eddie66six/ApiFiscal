using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Core.Domain.Afip.Entity.Returns
{
    [XmlRoot(ElementName = "FECAEDetResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FECAEDetResponse
    {
        [XmlElement(ElementName = "CAE", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string CAE { get; set; }
        [XmlElement(ElementName = "CAEFchVto", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string CAEFchVto { get; set; }
    }

    [XmlRoot(ElementName = "FeDetResp", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FeDetResp
    {
        [XmlElement(ElementName = "FECAEDetResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<FECAEDetResponse> FECAEDetResponse { get; set; }
    }

    [XmlRoot(ElementName = "FeCabResp", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FeCabResp
    {
        [XmlElement(ElementName = "Cuit")]
        public string Cuit { get; set; }
        [XmlElement(ElementName = "PtoVta")]
        public string PtoVta { get; set; }
        [XmlElement(ElementName = "CbteTipo")]
        public string CbteTipo { get; set; }
        [XmlElement(ElementName = "FchProceso")]
        public string FchProceso { get; set; }
        [XmlElement(ElementName = "CantReg")]
        public string CantReg { get; set; }
        [XmlElement(ElementName = "Resultado")]
        public string Resultado { get; set; }
        [XmlElement(ElementName = "Reproceso")]
        public string Reproceso { get; set; }
    }

    [XmlRoot(ElementName = "FECAESolicitarResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FECAESolicitarResult
    {
        [XmlElement(ElementName = "FeCabResp", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FeCabResp FeCabResp { get; set; }
        [XmlElement(ElementName = "FeDetResp", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FeDetResp FeDetResp { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
    }

    [XmlRoot(ElementName = "FECAESolicitarResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FECAESolicitarResponse
    {
        [XmlElement(ElementName = "FECAESolicitarResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FECAESolicitarResult FECAESolicitarResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFECAESolicitar
    {
        [XmlElement(ElementName = "FECAESolicitarResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FECAESolicitarResponse FECAESolicitarResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class FECAESolicitar
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFECAESolicitar Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

}
