using PosAppServer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppServer.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<TodoItem>? TodoItems { get; set; }
    }
}
