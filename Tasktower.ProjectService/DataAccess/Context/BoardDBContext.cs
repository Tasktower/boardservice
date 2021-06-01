using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tasktower.ProjectService.DataAccess.Entities.Base;
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
            modelBuilder.Entity<Entities.ProjectEntity>(Entities.ProjectEntity.BuildEntity);
            modelBuilder.Entity<Entities.ProjectRoleEntity>(Entities.ProjectRoleEntity.BuildEntity);
            modelBuilder.Entity<Entities.TaskBoardEntity>(Entities.TaskBoardEntity.BuildEntity);
            modelBuilder.Entity<Entities.TaskEntity>(Entities.TaskEntity.BuildEntity);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                UserContext userContext = UserContext.FromHttpContext(_httpContextAccessor?.HttpContext);
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = userContext.Name;
                }
                else
                {
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
                ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).ModifiedBy = userContext.Name;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Entities.ProjectEntity> Projects { get; set; }
        public DbSet<Entities.ProjectRoleEntity> ProjectRoles { get; set; }
        public DbSet<Entities.TaskBoardEntity> TaskBoards { get; set; }
        public DbSet<Entities.TaskEntity> Tasks { get; set; }
    }
}
