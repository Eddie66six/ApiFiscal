using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Core.Domain.Afip.Entity.Returns
{
    [XmlRoot(ElementName = "PtoVenta", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class PtoVenta
    {
        [XmlElement(ElementName = "Nro", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Nro { get; set; }

        [XmlElement(ElementName = "EmisionTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string EmisionTipo { get; set; }

        [XmlElement(ElementName = "Bloqueado", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Bloqueado { get; set; }

        [XmlElement(ElementName = "FchBaja", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string FchBaja { get; set; }
    }

    [XmlRoot(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class ResultGet
    {
        [XmlElement(ElementName = "PtoVenta", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<PtoVenta> PtoVenta { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetPtosVentaResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetPtosVentaResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGet ResultGet { get; set; }

        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }

        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetPtosVentaResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetPtosVentaResponse
    {
        [XmlElement(ElementName = "FEParamGetPtosVentaResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetPtosVentaResult FEParamGetPtosVentaResult { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "FEParamGetPtosVentaResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetPtosVentaResponse FEParamGetPtosVentaResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class FEParamGetPtosVenta
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }

        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }
}
