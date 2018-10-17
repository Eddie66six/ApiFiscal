using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "TributoTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class TributoTipo
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
    public class ResultGetFEParamGetTiposTributos
    {
        [XmlElement(ElementName = "TributoTipo", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<TributoTipo> TributoTipo { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposTributosResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposTributosResult
    {
        [XmlElement(ElementName = "ResultGet", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public ResultGetFEParamGetTiposTributos ResultGet { get; set; }
        [XmlElement(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Errors Errors { get; set; }
        [XmlElement(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public Events Events { get; set; }
    }

    [XmlRoot(ElementName = "FEParamGetTiposTributosResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class FEParamGetTiposTributosResponse
    {
        [XmlElement(ElementName = "FEParamGetTiposTributosResult", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposTributosResult FEParamGetTiposTributosResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyFEParamGetTiposTributos
    {
        [XmlElement(ElementName = "FEParamGetTiposTributosResponse", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public FEParamGetTiposTributosResponse FEParamGetTiposTributosResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeFEParamGetTiposTributos
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyFEParamGetTiposTributos Body { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }
}
