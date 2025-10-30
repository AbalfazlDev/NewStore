using NewStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace NewStore.Domain.Entities.Users
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserInRole> userInRoles { get; set; }
    }

}
