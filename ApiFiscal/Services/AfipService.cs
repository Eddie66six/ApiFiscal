using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ApiFiscal.Models;
using Newtonsoft.Json;
using wsaa;

namespace ApiFiscal.Services
{
    public class AfipService
    {
        public loginTicketResponse LoginAsync(string caminhoArquivoPfx, string senha)
        {
            var _senha = new NetworkCredential("", senha).SecurePassword;
            var _servico = "wsfe";
            var xmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

            var _globalUniqueID = 1;

            var XmlLoginTicketRequest = new XmlDocument();
            XmlLoginTicketRequest.LoadXml(xmlStrLoginTicketRequestTemplate);

            var xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
            var xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
            var xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
            var xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");
            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-2).ToString("s");
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+2).ToString("s");
            xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
            xmlNodoService.InnerText = _servico;

            X509Certificate2 objCert = new X509Certificate2(File.ReadAllBytes(caminhoArquivoPfx), _senha, X509KeyStorageFlags.PersistKeySet);

            Encoding EncodedMsg = Encoding.UTF8;
            byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);
            byte[] encodedSignedCms = FirmaBytesMensaje(msgBytes, objCert);

            var cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);

            var servicioWsaa = new LoginCMSClient();
            var result = servicioWsaa.loginCmsAsync(cmsFirmadoBase64).Result;

            XmlSerializer serializer = new XmlSerializer(typeof(loginTicketResponse));
            StringReader rdr = new StringReader(result.loginCmsReturn);
            var retorno = (loginTicketResponse)serializer.Deserialize(rdr);
            return retorno;
        }

        public string EmitirNotaAsync(string xml)
        {
            try
            {
                HttpClient client = new HttpClient();
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                HttpResponseMessage response = client.PostAsync("https://wswhomo.afip.gov.ar/wsfev1/service.asmx?op=FECAESolicitar", httpContent).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Models.Afip.Envelope ObterTipoIva(string xml)
        {
            try
            {
                HttpClient client = new HttpClient();
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                HttpResponseMessage response = client.PostAsync("https://wswhomo.afip.gov.ar/wsfev1/service.asmx?op=FEParamGetTiposIva", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                XmlSerializer serializer = new XmlSerializer(typeof(Models.Afip.Envelope));
                StringReader rdr = new StringReader(result);
                var resultingMessage = (Models.Afip.Envelope)serializer.Deserialize(rdr);

                return resultingMessage;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        private static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
        {
            const string ID_FNC = "[FirmaBytesMensaje]";
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
                ContentInfo infoContenido = new ContentInfo(argBytesMsg);
                SignedCms cmsFirmado = new SignedCms(infoContenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante
                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                // Encodeo el mensaje PKCS #7.
                return cmsFirmado.Encode();
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception(ID_FNC + "***Error al firmar: " + excepcionAlFirmar.Message);
            }
        }
    }
}