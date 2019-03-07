using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ApiFiscal.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiFiscal.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private readonly IErrorEvents _domainEvents;
        public BaseApiController(IErrorEvents domainEvents)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            _domainEvents = domainEvents;
        }
        public Task<ObjectResult> CreateResponse(object result, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return Task.FromResult(_domainEvents.IsMessage() ? StatusCode((int)HttpStatusCode.BadRequest, _domainEvents.GetMessages()) : StatusCode((int)httpStatusCode, result));
        }
    }
}