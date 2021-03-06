﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ApiFiscal.Models;
using ApiFiscal.Models.Afip;
using wsaa;

namespace ApiFiscal.Services
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
                var cmsFirmante = new CmsSigner(argCertFirmante) {IncludeOption = X509IncludeOption.EndCertOnly};

                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                // Encodeo el mensaje PKCS #7.
                return cmsFirmado.Encode();
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception(idFnc + "***Error al firmar: " + excepcionAlFirmar.Message);
            }
        }

        public loginTicketResponse LoginAsync(string caminhoArquivoPfx, string senha, ref string error)
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

            var objCert = new X509Certificate2(File.ReadAllBytes(caminhoArquivoPfx), senhaTmp, X509KeyStorageFlags.PersistKeySet);

            var encodedMsg = Encoding.UTF8;
            var msgBytes = encodedMsg.GetBytes(xmlLoginTicketRequest.OuterXml);
            var encodedSignedCms = FirmaBytesMensaje(msgBytes, objCert);

            var cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);

            var servicioWsaa = new LoginCMSClient();
            try
            {
                loginCmsResponse result = servicioWsaa.loginCmsAsync(cmsFirmadoBase64).Result;
                var serializer = new XmlSerializer(typeof(loginTicketResponse));
                var rdr = new StringReader(result.loginCmsReturn);
                var retorno = (loginTicketResponse)serializer.Deserialize(rdr);
                return retorno;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }

        public string EmitirNotaAsync(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FECAESolicitar", httpContent).Result;
                var retorno = response.Content.ReadAsStringAsync().Result;
                return retorno;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EnvelopeFEParamGetTiposIva ObterTipoIva(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FEParamGetTiposIva", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                XmlSerializer serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposIva));
                StringReader rdr = new StringReader(result);
                var resultingMessage = (EnvelopeFEParamGetTiposIva)serializer.Deserialize(rdr);

                return resultingMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EnvelopeFEParamGetTiposMonedas ObterTiposMonedas(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FEParamGetTiposMonedas", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposMonedas));
                var obj = (EnvelopeFEParamGetTiposMonedas)serializer.Deserialize(new StringReader(result));

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EnvelopeFEParamGetTiposCbte ObterTiposCbte(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FEParamGetTiposCbte", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposCbte));
                var obj = (EnvelopeFEParamGetTiposCbte)serializer.Deserialize(new StringReader(result));
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EnvelopeFEParamGetTiposDoc ObterTiposDoc(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "FEParamGetTiposDoc", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposDoc));
                var obj = (EnvelopeFEParamGetTiposDoc)serializer.Deserialize(new StringReader(result));

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EnvelopeFEParamGetTiposTributos ObterTiposTributos(string xml)
        {
            try
            {
                var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
                var response = _client.PostAsync(_urlApi + "=FEParamGetTiposTributos", httpContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var serializer = new XmlSerializer(typeof(EnvelopeFEParamGetTiposTributos));
                var obj = (EnvelopeFEParamGetTiposTributos)serializer.Deserialize(new StringReader(result));

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}