using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "FEHeaderInfo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEHeaderInfo
    {
        [XmlElement(ElementName = "ambiente", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Ambiente { get; set; }
        [XmlElement(ElementName = "fecha", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Fecha { get; set; }
        [XmlElement(ElementName = "id", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Header
    {
        [XmlElement(ElementName = "FEHeaderInfo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEHeaderInfo FEHeaderInfo { get; set; }
    }

    [XmlRoot(ElementName = "IvaTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class IvaTipo
    {
        [XmlElement(ElementName = "Id", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public int Id { get; set; }
        [XmlElement(ElementName = "Desc", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "FchDesde", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string FchDesde { get; set; }
        [XmlElement(ElementName = "FchHasta", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string FchHasta { get; set; }
    }

    [XmlRoot(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class ResultGetFEParamGetTiposIva
    {
        [XmlElement(ElementName = "IvaTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<IvaTipo> IvaTipo { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposIvaResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposIvaResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGetFEParamGetTiposIva ResultGet { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposIvaResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposIvaResponse
    {
        [XmlElement(ElementName = "FEParamGetTiposIvaResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposIvaResult FEParamGetTiposIvaResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFEParamGetTiposIva
    {
        [XmlElement(ElementName = "FEParamGetTiposIvaResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposIvaResponse FEParamGetTiposIvaResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeFEParamGetTiposIva
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFEParamGetTiposIva Body { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }
}
