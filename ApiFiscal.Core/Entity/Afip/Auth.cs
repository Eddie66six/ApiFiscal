namespace ApiFiscal.Core.Entity.Afip
{
    public sealed class Auth
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Token retornado pela WSAA</param>
        /// <param name="sign">Sinal retornado pela WSAA</param>
        /// <param name="cuit">Contribuinte Cuit (representado ou Emitente)</param>
        /// <param name="type">Tipo da requisicao</param>
        public static Auth Get(string token, string sign, long cuit, string type)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(type))
                return null;
            return new Auth(token, sign, cuit, type);
        }

        private Auth(string token, string sign, long cuit, string type)
        {
            Token = token;
            Sign = sign;
            Cuit = cuit;
            Type = type;
        }
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Retorna o xml de acordo com o Type para requisicoes
        /// </summary>
        /// <returns></returns>
        public string GetXmlAuth()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                      "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                          "<soap:Body>" +
                              "<" + Type + " xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                                  "<Auth>" +
                                      "<Token>" + Token + "</Token>" +
                                      "<Sign>" + Sign + "</Sign>" +
                                      "<Cuit>" + Cuit + "</Cuit>" +
                                  "</Auth>" +
                              "</" + Type + ">" +
                          "</soap:Body>" +
                      "</soap:Envelope>";
            return xml;
        }
    }
}