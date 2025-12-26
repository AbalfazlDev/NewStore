using NewStore.Domain.Entities.Common;
using NewStore.Domain.Entities.Order;
using System.Collections.Generic;

namespace NewStore.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public short? Age { get; set; }
        public string Password { get; set; }

        public ICollection<UserInRole> UserInRoles { get; set; }
        public ICollection<NewStore.Domain.Entities.Order.Order> Orders { get; set; }

    }

}
