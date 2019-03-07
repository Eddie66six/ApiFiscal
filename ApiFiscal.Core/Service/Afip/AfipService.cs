using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ApiFiscal.Core.Domain.Afip.Entity.Returns;
using wsaahomo;

namespace ApiFiscal.Core.Service.Afip
{
    public class AfipService
    {
        private readonly HttpClient _client;
        private readonly string _urlApi;
        public AfipService()
        {
            _client = new HttpClient();
            _urlApi = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx?op=";
        }

        private static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
        {
            const string idFnc = "[FirmaBytesMensaje]";
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
                var infoContenido = new ContentInfo(argBytesMsg);
                var cmsFirmado = new SignedCms(infoContenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante
                var cmsFirmante = new CmsSigner(argCertFirmante) { IncludeOption = X509IncludeOption.EndCertOnly };

                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                // Encodeo el mensaje PKCS #7.
                return cmsFirmado.Encode();
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception(idFnc + "***Error al firmar: " + excepcionAlFirmar.Message + " " + excepcionAlFirmar?.InnerException?.Message ?? "");
            }
        }

        public LoginAfipReturn LoginAsync(string caminhoArquivoPfx, string senha, ref string error)
        {
            var senhaTmp = new NetworkCredential("", senha).SecurePassword;
            var servico = "wsfe";
            var xmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

            var globalUniqueId = 1;

            var xmlLoginTicketRequest = new XmlDocument();
            xmlLoginTicketRequest.LoadXml(xmlStrLoginTicketRequestTemplate);

            var xmlNodoUniqueId = xmlLoginTicketRequest.SelectSingleNode("//uniqueId");
            var xmlNodoGenerationTime = xmlLoginTicketRequest.SelectSingleNode("//generationTime");
            var xmlNodoExpirationTime = xmlLoginTicketRequest.SelectSingleNode("//expirationTime");
            var xmlNodoService = xmlLoginTicketRequest.SelectSingleNode("//service");
            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-2).ToString("s");
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+2).ToString("s");
            xmlNodoUniqueId.InnerText = Convert.ToString(globalUniqueId);
            xmlNodoService.InnerText = servico;

            var arquivoPfx = DownloadBlob(caminhoArquivoPfx);
            if (arquivoPfx == null)
            {
                error = "Arquivo pfx não encontrado";
                return null;
            }

            //File.ReadAllBytes(caminhoArquivoPfx)
            var objCert = new X509Certificate2(arquivoPfx, senhaTmp, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

            var encodedMsg = Encoding.UTF8;
            var msgBytes = encodedMsg.GetBytes(xmlLoginTicketRequest.OuterXml);
            var encodedSignedCms = FirmaBytesMensaje(msgBytes, objCert);

            var cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);

            var servicioWsaa = new LoginCMSClient();
            try
            {
                loginCmsResponse result = servicioWsaa.loginCmsAsync(cmsFirmadoBase64).Result;
                var serializer = new XmlSerializer(typeof(LoginAfipReturn));
                var rdr = new StringReader(result.loginCmsReturn);
                var retorno = (LoginAfipReturn)serializer.Deserialize(rdr);
                return retorno;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }

        public FECAESolicitar EmitirNotaAsync(string xml, ref string error)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FECAESolicitar", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var serializer = new XmlSerializer(typeof(FECAESolicitar));
                var rdr = new StringReader(result);
                var retorno = (FECAESolicitar)serializer.Deserialize(rdr);
                return retorno;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }

        public FECompUltimoAutorizado UltimoNumeroAutorizado(string xml, ref string error)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FECAESolicitar", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var serializer = new XmlSerializer(typeof(FECompUltimoAutorizado));
                var rdr = new StringReader(result);
                var retorno = (FECompUltimoAutorizado)serializer.Deserialize(rdr);
                return retorno;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }

        private byte[] DownloadBlob(string path)
        {
            var request = WebRequest.Create(path.TrimEnd('/'));
            try
            {
                using (var response = request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    var buffer = new byte[16 * 1024];
                    using (var ms = new MemoryStream())
                    {
                        int read;
                        while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                 return null;
            }
        }

        //public EnvelopeFEParamGetTiposIva ObterTipoIva(string xml)
        //{
        //    try
        //    {
        //        var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
        //        var response = _client.PostAsync(_urlApi + "FEParamGetTiposIva", httpContent).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        XmlSerializer serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposIva));
        //        StringReader rdr = new StringReader(result);
        //        var resultingMessage = (EnvelopeFEParamGetTiposIva)serializer.Deserialize(rdr);

        //        return resultingMessage;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public EnvelopeFEParamGetTiposMonedas ObterTiposMonedas(string xml)
        //{
        //    try
        //    {
        //        var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
        //        var response = _client.PostAsync(_urlApi + "FEParamGetTiposMonedas", httpContent).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposMonedas));
        //        var obj = (EnvelopeFEParamGetTiposMonedas)serializer.Deserialize(new StringReader(result));

        //        return obj;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public EnvelopeFEParamGetTiposCbte ObterTiposCbte(string xml)
        //{
        //    try
        //    {
        //        var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
        //        var response = _client.PostAsync(_urlApi + "FEParamGetTiposCbte", httpContent).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposCbte));
        //        var obj = (EnvelopeFEParamGetTiposCbte)serializer.Deserialize(new StringReader(result));
        //        return obj;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public EnvelopeFEParamGetTiposDoc ObterTiposDoc(string xml)
        //{
        //    try
        //    {
        //        var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
        //        var response = _client.PostAsync(_urlApi + "FEParamGetTiposDoc", httpContent).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposDoc));
        //        var obj = (EnvelopeFEParamGetTiposDoc)serializer.Deserialize(new StringReader(result));

        //        return obj;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public EnvelopeFEParamGetTiposTributos ObterTiposTributos(string xml)
        //{
        //    try
        //    {
        //        var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
        //        var response = _client.PostAsync(_urlApi + "=FEParamGetTiposTributos", httpContent).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposTributos));
        //        var obj = (EnvelopeFEParamGetTiposTributos)serializer.Deserialize(new StringReader(result));

        //        return obj;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}