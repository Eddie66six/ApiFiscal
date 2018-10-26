namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class Auth : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Token retornado pela WSAA</param>
        /// <param name="sign">Sinal retornado pela WSAA</param>
        /// <param name="cuit">Contribuinte Cuit (representado ou Emitente)</param>
        /// <param name="type">Tipo da requisicao</param>
        public Auth(string token, string sign, long cuit)
        {
            Token = token;
            Sign = sign;
            Cuit = cuit;
        }
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }

        protected override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(Token) || string.IsNullOrEmpty(Sign))
            {
                IsValid = false;
            }
        }

        /// <summary>
        /// Retorna o xml de acordo com o Type para requisicoes
        /// </summary>
        /// <returns></returns>
        public string GetXmlAuth(string type)
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                      "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                          "<soap:Body>" +
                              "<" + type + " xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                                  "<Auth>" +
                                      "<Token>" + Token + "</Token>" +
                                      "<Sign>" + Sign + "</Sign>" +
                                      "<Cuit>" + Cuit + "</Cuit>" +
                                  "</Auth>" +
                              "</" + type + ">" +
                          "</soap:Body>" +
                      "</soap:Envelope>";
            return xml;
        }
    }
}