using System.Collections.Generic;
using System.Linq;
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
            //var login = afipApi.LoginAsync("C:\\Users\\Eddie\\Desktop\\certificado.pfx", "w12");

            var auth = new Auth("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iNDE5MTIxODgxNyIgZ2VuX3RpbWU9IjE1Mzk3OTY2OTUiIGV4cF90aW1lPSIxNTM5ODM5OTU1Ii8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjcxMjk2NjY2MTIsIENOPWd0ZXN0ZSIgYXV0aG1ldGhvZD0iY21zIiByZWdtZXRob2Q9IjIyIj4KICAgICAgICAgICAgPHJlbGF0aW9ucz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjI3MTI5NjY2NjEyIiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAgIDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==",
                "PIIMsGnPkWhKjgadit0SQaanMeGdqK5t8lJjAPdgsQP4veu6TNqwGg+gQ1WG5ZIlvc7BkOtpCfTPM2MZlSc53AnPis6eF9yYHxEMj3UAjCvOZBcju2HgvjmWDr63E8wwdhoifnavFCC88kgylI1Q/hkx0kGs0N2NyHDfH10kwsU=",
                cuit);
            var iva = afipApi.ObterTipoIva(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Ivas));
            var moeda = afipApi.ObterTiposMonedas(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Moedas));
            var cbte = afipApi.ObterTiposCbte(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.Cbte));
            var tiposDoc = afipApi.ObterTiposDoc(AfipCriarNotaFistal.GetXmlAuth(auth.Token, auth.Sign, cuit, EnumAfipPost.TiposDoc));

            var feCabReq = new FeCabReq(1, 1,1);
            var cbteAsoc = new List<CbteAsoc>() { new CbteAsoc(1, 1, 1) };//88, 991
            var tributo = new List<Tributo>() { new Tributo(1, 2.0, 1.0, 1.0, null) };
            var alicIva = new AlicIva(3, 0.1, 0.0);//iva.Body.FEParamGetTiposIvaResponse.FEParamGetTiposIvaResult.ResultGet.IvaTipo.Select(p=> new AlicIva(p.Id,0.1, 0.0)).ToList();
            var fECAEDetRequest = new FECAEDetRequest(3, EDocTipo.CUIT, 30710969619, 1, 1, 2.0, 2.0, 0.0, "PES", 1.0,
                null, new Tributos(tributo), new Iva(new List<AlicIva> { alicIva }), null);
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
