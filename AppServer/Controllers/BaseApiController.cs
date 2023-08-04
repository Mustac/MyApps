using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace PosAppServer.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {

        public int UserId { get
            {
                return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            } 
        }
        

        protected ActionResult AutoResponse<T>(ServerResponse<T> serverResponse) =>
            StatusCode((int)serverResponse.HttpStatusCode, serverResponse);

        protected ActionResult ServerError() => 
            StatusCode(500, new ServerResponse<object>(HttpStatusCode.InternalServerError, "Server Error"));
    }
}
