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
        /// <param name="pathPfx">Caminho do arquivo pfx</param>
        /// <param name="password">Senha do arquivo pfx</param>
        public static Auth Create(string token, string sign, long cuit, string pathPfx, string password)
        {
            var result = new Auth(token, sign, cuit, pathPfx, password);
            result.ValidateCreate();
            return result.IsValid ? result : null;
        }

        private Auth(string token, string sign, long cuit, string pathPfx, string password)
        {
            Token = token;
            Sign = sign;
            Cuit = cuit;
            PathPfx = pathPfx;
            Password = password;
        }
        private void ValidateCreate()
        {
            if (string.IsNullOrEmpty(PathPfx))
            {
                RaiseError("Caminho do arquivo pfx invalido");
                IsValid = false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                RaiseError("Password obrigatoria");
                IsValid = false;
            }
            if (Cuit.ToString().Length < 11)
            {
                RaiseError("Cuit deve ter no minimo 11 caracteres");
                IsValid = false;
            }
        }

        public bool IsLogged()
        {
            return !string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(Sign);
        }

        public string Token { get; private set; }
        public string Sign { get; private set; }
        public long Cuit { get; }
        public string PathPfx { get; }
        public string Password { get; }

        /// <summary>
        /// Retorna o xml de acordo com o Type para requisicoes, exeto para obter ultimo numero
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

        /// <summary>
        /// Retorna o xml de acordo com o Type para requisicoes
        /// </summary>
        /// <returns></returns>
        public string GetXmlAuthLastAuthorizedNumber(int ptoVta, int cbteTipo)
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                      "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                          "<soap:Body>" +
                              "<FECompUltimoAutorizado xmlns=\"http://ar.gov.afip.dif.FEV1/\">" +
                                  "<Auth>" +
                                      "<Token>" + Token + "</Token>" +
                                      "<Sign>" + Sign + "</Sign>" +
                                      "<Cuit>" + Cuit + "</Cuit>" +
                                  "</Auth>" +
                                  "<PtoVta>" + ptoVta + "</PtoVta>" +
                                  "<CbteTipo>" + cbteTipo + "</CbteTipo>" +
                              "</FECompUltimoAutorizado>" +
                          "</soap:Body>" +
                      "</soap:Envelope>";
            return xml;
        }

        public void UpdateCredencial(string token, string sign)
        {
            Token = token;
            Sign = sign;
            if (!string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(Sign)) return;
            RaiseError("Token/Sign obrigatorio");
            IsValid = false;
        }
    }
}