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

        [HttpPost("item/create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateItemAsync(TodoItemRegistration todoItemRegistration)
        {
            try
            {
                var response = await _todoService.CreateItemAsync(todoItemRegistration, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }


        [HttpPut("item/update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateItemAsync(TodoItemInfo todoItemInfo)
        {
            try
            {
                var response = await _todoService.UpdateItemAsync(todoItemInfo, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }

        [HttpPut("list/update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateListAsync(TodoListInfo todoListInfo)
        {
            try
            {
                var response = await _todoService.UpdateListAsync(todoListInfo, UserId);
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

        [HttpDelete("item/delete/{itemid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteItemAsync(int itemid)
        {
            try
            {
                var response = await _todoService.DeleteItemAsync(itemid, UserId);
                return AutoResponse(response);
            }
            catch
            {
                return ServerError();
            }
        }
    }
}
