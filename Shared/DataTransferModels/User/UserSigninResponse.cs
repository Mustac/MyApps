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
        public string JwtToken { get; set; }
        public bool FirstSignin { get; set; }

    }
}

