using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "DocTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class DocTipo
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
    public class ResultGetFEParamGetTiposDoc
    {
        [XmlElement(ElementName = "DocTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<DocTipo> DocTipo { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposDocResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposDocResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGetFEParamGetTiposDoc ResultGet { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposDocResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposDocResponse
    {
        [XmlElement(ElementName = "FEParamGetTiposDocResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposDocResult FEParamGetTiposDocResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFEParamGetTiposDoc
    {
        [XmlElement(ElementName = "FEParamGetTiposDocResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposDocResponse FEParamGetTiposDocResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeFEParamGetTiposDoc
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFEParamGetTiposDoc Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }
}
