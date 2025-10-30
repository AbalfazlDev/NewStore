using NewStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Domain.Entities.Users
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Password { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }

}
