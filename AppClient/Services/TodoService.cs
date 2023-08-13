using Microsoft.AspNetCore.Components;
using MyAppClient.Helpers;
using Shared.DataTransferModels.Todo;

namespace MyAppClient.Services
{
    public class TodoService 
    {
        private readonly ApiCallService _apiCallService;

        public TodoService(ApiCallService apiCallService)
        {
            _apiCallService = apiCallService;

        }

        public async Task<ServerResponse<object>> CreateNewListAsnyc(TodoListRegistration todoList) =>
             await _apiCallService.ApiPostAsync(todoList, "todo/list/create", true);

        public async Task<ServerResponse<IEnumerable<TodoListInfo>>> GetTodoListsAsync() =>
            await _apiCallService.ApiGetAsync<IEnumerable<TodoListInfo>>("todo/list/all", false);

        public async Task<ServerResponse<IEnumerable<TodoItemInfo>>> GetTodoItemsByIdAsync(int id) =>
            await _apiCallService.ApiGetAsync<IEnumerable<TodoItemInfo>>($"todo/item/bylistid/{id}");

        public async Task<ServerResponse<object>> DeleteListAsync(int id) =>
            await _apiCallService.ApiDeleteAsync($"todo/list/delete/{id}", true);

        public async Task<ServerResponse<object>> UpdateItemAsync(TodoItemInfo todoItemInfo) =>
            await _apiCallService.ApiPutAsync(todoItemInfo, $"todo/item/update");

        public async Task<ServerResponse<object>> UpdateListAsync(TodoListInfo todoListInfo) =>
            await _apiCallService.ApiPutAsync(todoListInfo, $"todo/list/update");

        public async Task<ServerResponse<object>> CreateItemAsync(TodoItemRegistration todoItemRegistration) =>
            await _apiCallService.ApiPostAsync(todoItemRegistration, $"todo/item/create");

        public async Task<ServerResponse<object>> DeleteItemAsync(int id) =>
            await _apiCallService.ApiDeleteAsync($"todo/item/delete/{id}", true);
    }
}
