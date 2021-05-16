using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.DataAccess.Context
{
    public class BoardDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BoardDBContext(DbContextOptions<BoardDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Project>(Entities.Project.BuildEntity);
            modelBuilder.Entity<Entities.ProjectRole>(Entities.ProjectRole.BuildEntity);
            modelBuilder.Entity<Entities.TaskBoard>(Entities.TaskBoard.BuildEntity);
            modelBuilder.Entity<Entities.Task>(Entities.Task.BuildEntity);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entities.BaseAuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                UserContext userContext = UserContext.FromHttpContext(_httpContextAccessor?.HttpContext);
                if (entityEntry.State == EntityState.Added)
                {
                    ((Entities.BaseAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Entities.BaseAuditableEntity)entityEntry.Entity).CreatedBy = userContext.Name ?? "ANONYMOUS";
                }
                else
                {
                    Entry((Entities.BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Entities.BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
                ((Entities.BaseAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((Entities.BaseAuditableEntity)entityEntry.Entity).ModifiedBy = userContext.Name ?? "ANONYMOUS";
                ((Entities.BaseAuditableEntity)entityEntry.Entity).Version = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Entities.Project> Projects { get; set; }
        public DbSet<Entities.ProjectRole> ProjectRoles { get; set; }
        public DbSet<Entities.TaskBoard> TaskBoards { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
    }
}
