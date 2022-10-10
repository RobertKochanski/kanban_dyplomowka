using Duende.IdentityServer.EntityFramework.Options;
using KanbanDAL.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KanbanDAL
{
    public class KanbanDbContext : ApiAuthorizationDbContext<User>
    {
        public KanbanDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Board> boards { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Job> jobs { get; set; }
    }
}
