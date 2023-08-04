using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppServer.Services;
using PosAppServer.Controllers;
using Shared.DataTransferModels.Todo;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyAppServer.Controllers
{
    [Route("api/todo")]
    [ApiController]
    [Authorize]
    public class TodoController : BaseApiController
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("list/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateNewListAsync(TodoListRegistration todoList)
        {
            try
            {
                var response = await _todoService.CreateNewListAsync(todoList.Name, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }


        }

       


     
    }
}
