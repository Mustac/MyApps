using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppServer.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }


        [ForeignKey("TodoList")]
        public int TodoListId { get; set; }
        public TodoList? TodoList { get; set; }
    }
}
