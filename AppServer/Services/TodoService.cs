using MyAppServer.Models;

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
    }
}
