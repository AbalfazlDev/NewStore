using Microsoft.EntityFrameworkCore;
using NewStore.Domain.Entities.Product;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewStore.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UsersInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
