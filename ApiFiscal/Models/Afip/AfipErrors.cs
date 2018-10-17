using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApiFiscal.Models.Afip
{
    [XmlRoot(ElementName = "Err", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class Err
    {
        [XmlElement(ElementName = "Code", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Msg", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Msg { get; set; }
    }

    [XmlRoot(ElementName = "Errors", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class Errors
    {
        [XmlElement(ElementName = "Err", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<Err> Err { get; set; }
    }

    [XmlRoot(ElementName = "Evt", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class Evt
    {
        [XmlElement(ElementName = "Code", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Msg", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public string Msg { get; set; }
    }

    [XmlRoot(ElementName = "Events", Namespace = "http://ar.gov.afip.dif.FEV1/")]
    public class Events
    {
        [XmlElement(ElementName = "Evt", Namespace = "http://ar.gov.afip.dif.FEV1/")]
        public List<Evt> Evt { get; set; }
    }
}
