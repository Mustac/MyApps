using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PosAppServer.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("send-verification-code")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> SendVerificationCode(UserEmail userEmail)
        {
            try
            {
                var response = await _accountService.VerifyEmailAsync(userEmail.Email);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }

        }


        [HttpPost("signin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> SignInUser(UserEmailCode userEmailCode)
        {
            try
            {
                var response = await _accountService.SignInUserAsync(userEmailCode);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }

        }

    }
}
