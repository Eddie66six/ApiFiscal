using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "Moneda", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class Moneda
    {
        [XmlElement(ElementName = "Id", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Desc", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "FchDesde", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string FchDesde { get; set; }
        [XmlElement(ElementName = "FchHasta", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string FchHasta { get; set; }
    }

    [XmlRoot(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class ResultGetFEParamGetTiposMonedas
    {
        [XmlElement(ElementName = "Moneda", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<Moneda> Moneda { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposMonedasResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposMonedasResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGetFEParamGetTiposMonedas ResultGet { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposMonedasResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposMonedasResponse
    {
        [XmlElement(ElementName = "FEParamGetTiposMonedasResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposMonedasResult FEParamGetTiposMonedasResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFEParamGetTiposMonedas
    {
        [XmlElement(ElementName = "FEParamGetTiposMonedasResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposMonedasResponse FEParamGetTiposMonedasResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeFEParamGetTiposMonedas
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFEParamGetTiposMonedas Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

}
