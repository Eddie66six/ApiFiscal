using System.Xml.Serialization;

namespace ApiFiscal.Core.Domain.Afip.Entity.Returns
{
    [XmlRoot(ElementName = "header")]
    public class Header
    {
        [XmlElement(ElementName = "source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "destination")]
        public string Destination { get; set; }
        [XmlElement(ElementName = "uniqueId")]
        public string UniqueId { get; set; }
        [XmlElement(ElementName = "generationTime")]
        public string GenerationTime { get; set; }
        [XmlElement(ElementName = "expirationTime")]
        public string ExpirationTime { get; set; }
    }

    [XmlRoot(ElementName = "credentials")]
    public class Credentials
    {
        [XmlElement(ElementName = "token")]
        public string Token { get; set; }
        [XmlElement(ElementName = "sign")]
        public string Sign { get; set; }
    }

    [XmlRoot(ElementName = "loginTicketResponse")]
    public class LoginAfipReturn
    {
        [XmlElement(ElementName = "header")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "credentials")]
        public Credentials Credentials { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

}