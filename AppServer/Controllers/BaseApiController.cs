using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace PosAppServer.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult AutoResponse<T>(ServerResponse<T> serverResponse) =>
            StatusCode((int)serverResponse.HttpStatusCode, serverResponse);

        protected ActionResult ServerError() => 
            StatusCode(500, new ServerResponse<object>(HttpStatusCode.InternalServerError, "Server Error"));
    }
}
