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
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
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

        [HttpGet("list/all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetListsAsync()
        {
            try
            {
                var response = await _todoService.GetTodoListsInfoAsync(UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }

        [HttpGet("item/bylistid/{listid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetItemsByListIdAsync(int listId)
        {
            try
            {
                var response = await _todoService.GetTodoItemsByListIdAsync(listId, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }


        [HttpDelete("list/delete/{listid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteListAsync(int listId)
        {
            try
            {
                var response = await _todoService.DeleteListAsync(listId, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }


    }
}
