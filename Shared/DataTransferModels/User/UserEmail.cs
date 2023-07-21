using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferModels.User
{
    public class UserEmail
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string Email { get; set; } = string.Empty;
    }

    public class UserEmailCode : UserEmail
    {
        [Required]
        public string Code { get; set; }
    }
}
