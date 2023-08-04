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

    }
}
