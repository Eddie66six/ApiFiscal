using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "CbteTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class CbteTipo
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
    public class ResultGetFEParamGetTiposCbte
    {
        [XmlElement(ElementName = "CbteTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<CbteTipo> CbteTipo { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposCbteResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposCbteResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGetFEParamGetTiposCbte ResultGet { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposCbteResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposCbteResponse
    {
        [XmlElement(ElementName = "FEParamGetTiposCbteResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposCbteResult FEParamGetTiposCbteResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFEParamGetTiposCbte
    {
        [XmlElement(ElementName = "FEParamGetTiposCbteResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposCbteResponse FEParamGetTiposCbteResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeFEParamGetTiposCbte
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFEParamGetTiposCbte Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }
}
