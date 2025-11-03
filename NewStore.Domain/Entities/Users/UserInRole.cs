using NewStore.Domain.Entities.Common;

namespace NewStore.Domain.Entities.Users
{
    public class UserInRole : BaseEntity
    {
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }

        public virtual User User { get; set; }
        public long UserId { get; set; }
    }
}
