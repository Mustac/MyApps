using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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


        [HttpPost("email-verification-code")]
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


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateUser(UserUpdate userUpdate)
        {
            try
            {
                var userEmail = HttpContext.Items["UserEmail"].ToString();
                var response = await _accountService.UpdateUserInfoAsync(userUpdate, userEmail);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }

        }



    }
}
