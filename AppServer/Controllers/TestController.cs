using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace PosAppServer.Controllers
{
    [ApiController] 
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult TestServerConnection() => 
            Ok(new ServerResponse<object>(HttpStatusCode.OK, "Success")); 
    }
}
 