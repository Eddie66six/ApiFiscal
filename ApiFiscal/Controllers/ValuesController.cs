using System;
using System.Collections.Generic;
using System.Linq;
using ApiFiscal.Core.Entity.Afip;
using ApiFiscal.Models;
using ApiFiscal.Models.Afip;
using ApiFiscal.Services;
using Microsoft.AspNetCore.Mvc;
using Auth = ApiFiscal.Models.Auth;

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

            return null;
            //recebe
            //blob .pfx
            //senha
            //cuit empresa
            //id iva
            //valor total
            //tipo documento comprador
            //documento comprador
            //opcional -----------
            //token
            //sign

            //retorno
            //<CbteFch>20181022</CbteFch>
            //<CAE>68432689021807</CAE>
            //<CAEFchVto>20181101</CAEFchVto>
            //token
            //sign

            var cuit = 27129666612;
            var afipApi = new AfipService();
            //var login = afipApi.LoginAsync("..\\teste\\certificado.pfx", "w12");

            var auth = new Auth("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iMTAwNzM0ODgxMCIgZ2VuX3RpbWU9IjE1NDAyMjc0NjMiIGV4cF90aW1lPSIxNTQwMjcwNzIzIi8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjcxMjk2NjY2MTIsIENOPWd0ZXN0ZSIgYXV0aG1ldGhvZD0iY21zIiByZWdtZXRob2Q9IjIyIj4KICAgICAgICAgICAgPHJlbGF0aW9ucz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjI3MTI5NjY2NjEyIiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAgIDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==",
                "VaMwR8K16MSUOZZeirXusddd92mUcuBopfLrHhvuwP0r9Qa5GvkKLoJBmN6unMOlYUzD02C6MLXHST8h6LGEIxDw1nCNZ8py6kNEfLc/6tAJglZvpX2MfzkhHo09tYhDDWVBciYjgLiSlxrcamsaZhdDwrmrzJQ+RzNdn2ufX3I=",
                cuit);
            var iva = afipApi.ObterTipoIva(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Ivas));
            var moeda = afipApi.ObterTiposMonedas(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Moedas));
            var cbte = afipApi.ObterTiposCbte(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Cbte));//So usamos 1,3,6,8 - Factura A, Nota de Crédito A, Factura B, Nota de Crédito B
            var tiposDoc = afipApi.ObterTiposDoc(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposDoc));
            var tiposTributos = afipApi.ObterTiposTributos(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposTributos));

            var feCabReq = new FeCabReq(1, 1,1);
            //var cbteAsoc = new List<CbteAsoc>() { new CbteAsoc(1, 1, 1) };//88, 991 //Emissão de factura não enviar essa informação, usada pra notas de crédito
            //var tributo = new List<Tributo>() { new Tributo(1, 2.0, 1.0, 1.0, null) };

            var alicIva = new AlicIva(5, 150,21.00, true);

            var fECAEDetRequest = new FECAEDetRequest(3, EDocTipo.Cuit, 30710969619, 2, 2, 2.0, 2.0, alicIva.Importe, "PES", 1.0,
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
        public void Post([FromBody] string value)
        {
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
