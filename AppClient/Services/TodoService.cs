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
    }
}
