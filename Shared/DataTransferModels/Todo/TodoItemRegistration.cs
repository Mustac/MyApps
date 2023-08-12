using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferModels.Todo
{
    public class TodoItemRegistration
    {
        [Required]
        [StringLength(maximumLength:20, MinimumLength = 2, ErrorMessage ="Task must be from 2 to 20 character length")]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
