using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferModels.User
{
    

    public class UserUpdate
    {
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Name must be from 3 to 20 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Last name must be from 3 to 20 characters")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Company name must be from 3 to 20 characters")]
        public string Company { get; set; } = string.Empty;
    }

}
