using System.Xml.Serialization;

namespace ApiFiscal.Core.Domain.Afip.Entity.Returns
{
    [XmlRoot(ElementName = "FECompUltimoAutorizadoResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FECompUltimoAutorizadoResult
    {
        [XmlElement(ElementName = "PtoVta", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public int PtoVta { get; set; }
        [XmlElement(ElementName = "CbteTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public int CbteTipo { get; set; }
        [XmlElement(ElementName = "CbteNro", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public int CbteNro { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FECompUltimoAutorizadoResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FECompUltimoAutorizadoResponse
    {
        [XmlElement(ElementName = "FECompUltimoAutorizadoResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FECompUltimoAutorizadoResult FECompUltimoAutorizadoResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFECompUltimoAutorizado
    {
        [XmlElement(ElementName = "FECompUltimoAutorizadoResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FECompUltimoAutorizadoResponse FECompUltimoAutorizadoResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class FECompUltimoAutorizado
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFECompUltimoAutorizado Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

}