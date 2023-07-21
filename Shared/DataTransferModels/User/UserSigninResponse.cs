using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferModels.User
{
    public class UserSigninResponse
    {
        public string Email { get; set; }
        public string ConfirmationKey { get; set; }

    }
}

