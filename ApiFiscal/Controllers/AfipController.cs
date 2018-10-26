using ApiFiscal.Core.Application.Afip;
using ApiFiscal.Core.Application.Afip.ModelreceiveParameters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfipController : BaseApiController
    {
        private readonly SendApp sendApp;
        public AfipController()
        {
            sendApp = new SendApp();
        }
        [Route("v1/emitir-nota")]
        [HttpPost]
        public Task<ObjectResult> Emitir(SendModel sendModel)
        {
            return CreateResponse(sendApp.Send(sendModel));
        }
    }
}