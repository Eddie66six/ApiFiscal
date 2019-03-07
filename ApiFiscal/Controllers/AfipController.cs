using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiFiscal.Core;
using ApiFiscal.Core.Application.Afip.Model;
using ApiFiscal.Core.Domain.Afip.Interfaces.Application;

namespace ApiFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfipController : BaseApiController
    {
        private readonly ISendApp _sendApp;
        public AfipController(IErrorEvents domainEvents, ISendApp sendApp) : base(domainEvents)
        {
            _sendApp = sendApp;
        }

        [Route("v1/emitir-nota")]
        [HttpPost]
        public Task<ObjectResult> Emitir(SendModel sendModel)
        {
            return CreateResponse(_sendApp.Send(sendModel));
        }
        [Route("v1/teste")]
        [HttpPost]
        public Task<ObjectResult> Teste()
        {
            return CreateResponse(new { generationTime = DateTime.Now.AddMinutes(-2).ToString("s") });
        }
    }
}