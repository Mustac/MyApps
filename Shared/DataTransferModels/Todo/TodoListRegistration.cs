using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferModels.Todo
{
    public class TodoListRegistration
    {
        [Required]
        [StringLength(maximumLength:30, MinimumLength = 2, ErrorMessage = "* Needs to be from 2 to 30 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
