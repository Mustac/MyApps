using MyAppServer.Models;
using Shared.DataTransferModels.Todo;
using System.Reflection.Metadata.Ecma335;

namespace MyAppServer.Services
{
    public class TodoService
    {
        private readonly AppDbContext _db;

        public TodoService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ServerResponse<object>> CreateNewListAsync(string name, int Id)
        {
            var list = await _db.TodoLists.FirstOrDefaultAsync(x => x.Name == name && x.Id == Id);

            if (list is not null)
                return new ServerResponse<object>(HttpStatusCode.Conflict, "List with that name already exist");

            TodoList todo = new TodoList()
            {
                Name = name,
                CreatedAt = DateTime.Now,
                IsCompleted = false,
                UserId = Id,
            };

            await _db.TodoLists.AddAsync(todo);

            return await _db.SaveChangesAsync() > 0 ?
                new ServerResponse<object>(HttpStatusCode.Created, "New List Created") :
                new ServerResponse<object>(HttpStatusCode.ServiceUnavailable, "Unable to create new list, try again later");

        }

        public async Task<ServerResponse<IEnumerable<TodoListInfo>>> GetTodoListsInfoAsync(int id)
        {
            var lists = await _db.TodoLists.Select(x =>
            new TodoListInfo
            {
                Id = x.Id,
                DateCreated = x.CreatedAt,
                IsCompleted = x.IsCompleted,
                Name = x.Name
            }
            ).ToListAsync();

            return lists is not null ?
                new ServerResponse<IEnumerable<TodoListInfo>>(HttpStatusCode.OK, "Success", lists) :
                new ServerResponse<IEnumerable<TodoListInfo>>(HttpStatusCode.ServiceUnavailable, "Unable to read from database, try again later");
        }

        public async Task<ServerResponse<IEnumerable<TodoItemInfo>>> GetTodoItemsByListIdAsync(int listId, int userId)
        {
            var items = await _db.TodoItems
                .Where(x => x.TodoListId == listId && x.TodoList.UserId == userId)
                .Select(x =>
                    new TodoItemInfo
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsCompleted = x.IsCompleted
                    }
                ).ToListAsync();


            return items is not null ?
                new ServerResponse<IEnumerable<TodoItemInfo>>(HttpStatusCode.OK, "Success", items) :
                new ServerResponse<IEnumerable<TodoItemInfo>>(HttpStatusCode.ServiceUnavailable, "Unable to read from database, try again later");

        }

        public async Task<ServerResponse<object>> DeleteListAsync(int listId, int userId)
        {
            var list = await _db.TodoLists.FirstOrDefaultAsync(x=> listId== x.Id && x.UserId == userId);

            if (list is null)
                return new ServerResponse<object>(HttpStatusCode.BadRequest, "Could not find that list");

            _db.TodoLists.Remove(list);

            return await _db.SaveChangesAsync() > 0 ?
                new ServerResponse<object>(HttpStatusCode.OK, $"List > {list.Name} < has been removed ") :
                new ServerResponse<object>(HttpStatusCode.InternalServerError, "Server error, try again later");
        }
    }
}
