using ApiFiscal.Models;
using ApiFiscal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfipController : BaseApiController
    {
        [Route("v1/emitir-nota")]
        [HttpPost]
        public Task<ObjectResult> EmitirNota(Auth auth)
        {
            var afipApi = new AfipService();
            if (auth == null)
                return CreateResponse(null);
            if(auth.Token == null || auth.Sign == null)
            {
                var login = afipApi.LoginAsync("..\\teste\\certificado.pfx", "w12");
                auth = new Auth(login.credentials.token, login.credentials.sign, auth.Cuit);
                //TODO valida e retorna caso de erro
            }

            var feCabReq = new FeCabReq(1, 1, 1);
            var alicIva = new AlicIva(5, 150, 21.00, true);

            var fECAEDetRequest = new FECAEDetRequest(3, EDocTipo.Cuit, 30710969619, 2, 2, 2.0, 2.0, alicIva.Importe, "PES", 1.0,
                null, null, new Iva(new List<AlicIva> { alicIva }), null);
            var objXml = new Envelope(auth, feCabReq, fECAEDetRequest);
            var xmlStr = AfipCriarNotaFistal.GetXml(objXml);


            afipApi.EmitirNotaAsync(xmlStr);
            return CreateResponse(new { a=1,b=2 });
        }
    }
}