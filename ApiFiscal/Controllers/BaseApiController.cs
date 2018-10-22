using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiFiscal.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public Task<ObjectResult> CreateResponse(object result, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return Task.FromResult(StatusCode((int)httpStatusCode, result));
        }
    }
}