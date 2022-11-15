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

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Boards
            modelBuilder.Entity<Board>()
                .Property(x => x.Name)
                .IsRequired();

            // Columns
            modelBuilder.Entity<Column>()
                .Property(x => x.Name)
                .IsRequired();

            // Jobs
            modelBuilder.Entity<Job>()
                .Property(x => x.Name)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
