using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAppServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ConfirmationCode { get; set; } = string.Empty;
        public bool FirstLogin { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; } 

    }
}
