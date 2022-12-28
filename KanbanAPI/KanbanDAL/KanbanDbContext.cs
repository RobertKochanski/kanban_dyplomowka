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
        public DbSet<Comment> Comments { get; set; }

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

            modelBuilder.Entity<Column>()
                .HasIndex(x => x.Name)
                .IsUnique();

            // Jobs
            modelBuilder.Entity<Job>()
                .Property(x => x.Name)
                .IsRequired();

            // Users
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();

            // Comments
            modelBuilder.Entity<Comment>()
                .Property(x => x.Text)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(x => x.Creator)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
