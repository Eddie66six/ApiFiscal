using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ApiFiscal.Models;
using ApiFiscal.Models.Afip;
using ApiFiscal.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var cuit = 27129666612;
            var afipApi = new AfipService();
            //var login = afipApi.LoginAsync("C:\\Users\\Gui\\Desktop\\certificado.pfx", "w12");
            //var login = afipApi.LoginAsync("C:\\DEV\\certificado.pfx", "w12");

            var auth = new Auth("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iNDE5MTIxODgxNyIgZ2VuX3RpbWU9IjE1Mzk3OTY2OTUiIGV4cF90aW1lPSIxNTM5ODM5OTU1Ii8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjcxMjk2NjY2MTIsIENOPWd0ZXN0ZSIgYXV0aG1ldGhvZD0iY21zIiByZWdtZXRob2Q9IjIyIj4KICAgICAgICAgICAgPHJlbGF0aW9ucz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjI3MTI5NjY2NjEyIiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAgIDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==",
                "PIIMsGnPkWhKjgadit0SQaanMeGdqK5t8lJjAPdgsQP4veu6TNqwGg+gQ1WG5ZIlvc7BkOtpCfTPM2MZlSc53AnPis6eF9yYHxEMj3UAjCvOZBcju2HgvjmWDr63E8wwdhoifnavFCC88kgylI1Q/hkx0kGs0N2NyHDfH10kwsU=",
                cuit);
            var iva = afipApi.ObterTipoIva(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Ivas));
            var moeda = afipApi.ObterTiposMonedas(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Moedas));
            var cbte = afipApi.ObterTiposCbte(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Cbte));//So usamos 1,3,6,8 - Factura A, Nota de Crédito A, Factura B, Nota de Crédito B
            var tiposDoc = afipApi.ObterTiposDoc(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposDoc));
            var tiposTributos = afipApi.ObterTiposTributos(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposTributos));

            var feCabReq = new FeCabReq(1, 1,1);
            //var cbteAsoc = new List<CbteAsoc>() { new CbteAsoc(1, 1, 1) };//88, 991 //Emissão de factura não enviar essa informação, usada pra notas de crédito
            //var tributo = new List<Tributo>() { new Tributo(1, 2.0, 1.0, 1.0, null) };



            //var alicIva = new AlicIva(3, 0.1, 0.0);//iva.Body.FEParamGetTiposIvaResponse.FEParamGetTiposIvaResult.ResultGet.IvaTipo.Select(p=> new AlicIva(p.Id,0.1, 0.0)).ToList();

            var percIVA = Math.Round((21.00 / 100), 2) + 1;
            var baseImp = Math.Round((150 / percIVA), 2);

            var impIVA = Math.Round(((21.00 / 100) * baseImp), 2);

            var alicIva = new AlicIva(5, Convert.ToDecimal(baseImp), Convert.ToDecimal(impIVA));//iva.Body.FEParamGetTiposIvaResponse.FEParamGetTiposIvaResult.ResultGet.IvaTipo.Select(p=> new AlicIva(p.Id,0.1, 0.0)).ToList();

            var fECAEDetRequest = new FECAEDetRequest(3, EDocTipo.CUIT, 30710969619, 2, 2, 2.0, 2.0, impIVA, "PES", 1.0,
                null, null, new Iva(new List<AlicIva> { alicIva }), null);
            var objXml = new Envelope(auth, feCabReq, fECAEDetRequest);

            var xmlStr = AfipCriarNotaFistal.GetXml(objXml);
            return afipApi.EmitirNotaAsync(xmlStr);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("v1/nf/enviar")]
        public HttpResponseMessage Post([FromBody] object objEnvio)
        {
            var cuit = 27129666612;
            var afipApi = new AfipService();
            //var login = afipApi.LoginAsync("C:\\Users\\Gui\\Desktop\\certificado.pfx", "w12");
            //var login = afipApi.LoginAsync("C:\\DEV\\certificado.pfx", "w12");

            var auth = new Auth("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iNDE5MTIxODgxNyIgZ2VuX3RpbWU9IjE1Mzk3OTY2OTUiIGV4cF90aW1lPSIxNTM5ODM5OTU1Ii8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjcxMjk2NjY2MTIsIENOPWd0ZXN0ZSIgYXV0aG1ldGhvZD0iY21zIiByZWdtZXRob2Q9IjIyIj4KICAgICAgICAgICAgPHJlbGF0aW9ucz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjI3MTI5NjY2NjEyIiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAgIDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==",
                "PIIMsGnPkWhKjgadit0SQaanMeGdqK5t8lJjAPdgsQP4veu6TNqwGg+gQ1WG5ZIlvc7BkOtpCfTPM2MZlSc53AnPis6eF9yYHxEMj3UAjCvOZBcju2HgvjmWDr63E8wwdhoifnavFCC88kgylI1Q/hkx0kGs0N2NyHDfH10kwsU=",
                cuit);
            var iva = afipApi.ObterTipoIva(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Ivas));
            var moeda = afipApi.ObterTiposMonedas(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Moedas));
            var cbte = afipApi.ObterTiposCbte(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Cbte));//So usamos 1,3,6,8 - Factura A, Nota de Crédito A, Factura B, Nota de Crédito B
            var tiposDoc = afipApi.ObterTiposDoc(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposDoc));
            var tiposTributos = afipApi.ObterTiposTributos(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposTributos));

            var feCabReq = new FeCabReq(1, 1, 1);
            //var cbteAsoc = new List<CbteAsoc>() { new CbteAsoc(1, 1, 1) };//88, 991 //Emissão de factura não enviar essa informação, usada pra notas de crédito
            //var tributo = new List<Tributo>() { new Tributo(1, 2.0, 1.0, 1.0, null) };
            //var alicIva = new AlicIva(3, 0.1, 0.0);//iva.Body.FEParamGetTiposIvaResponse.FEParamGetTiposIvaResult.ResultGet.IvaTipo.Select(p=> new AlicIva(p.Id,0.1, 0.0)).ToList();

            var env = (NF_ENVIO)objEnvio;
            decimal percIVA = 0;
            decimal baseImp = 0;
            decimal impIVA = 0;

            if (env._AR_VALOR_IVA != null && env._AR_VALOR_IVA > 0)
            {
                percIVA = Math.Round((env._AR_VALOR_IVA.GetValueOrDefault(0) / 100), 2) + 1;
                baseImp = Math.Round((env._VALOR_EMITIDO.GetValueOrDefault(0) / percIVA), 2);
                impIVA = Math.Round(((env._AR_VALOR_IVA.GetValueOrDefault(0) / 100) * baseImp), 2);
            }

            var alicIva = new AlicIva(env._AR_TIPO_IVA.GetValueOrDefault(5), baseImp, impIVA);//iva.Body.FEParamGetTiposIvaResponse.FEParamGetTiposIvaResult.ResultGet.IvaTipo.Select(p=> new AlicIva(p.Id,0.1, 0.0)).ToList();

            if (env._AR_NUMERO_DOCUMENTO == null)
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                responseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
                return responseMessage;
            }

            long nmrDocumento = 0;
            long.TryParse(env._AR_NUMERO_DOCUMENTO, out nmrDocumento);

            var fECAEDetRequest = new FECAEDetRequest(3, (EDocTipo) env._AR_TIPO_DOCUMENTO.GetValueOrDefault(2), nmrDocumento, 2, 2, 2.0, 2.0, Convert.ToDouble(impIVA), "PES", 1.0,
                null, null, new Iva(new List<AlicIva> { alicIva }), null);
            var objXml = new Envelope(auth, feCabReq, fECAEDetRequest);

            var xmlStr = AfipCriarNotaFistal.GetXml(objXml);
            return new HttpResponseMessage { Content = new StringContent(afipApi.EmitirNotaAsync(xmlStr)) };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
