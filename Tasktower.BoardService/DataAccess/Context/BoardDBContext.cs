using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.Security;

namespace Tasktower.BoardService.DataAccess.Context
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
            modelBuilder.Entity<TaskBoard>(TaskBoard.BuildEntity);
            modelBuilder.Entity<UserTaskBoardRole>(UserTaskBoardRole.BuildEntity);
            modelBuilder.Entity<TaskBoardColumn>(TaskBoardColumn.BuildEntity);
            modelBuilder.Entity<TaskCard>(TaskCard.BuildEntity);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                UserContext userContext = new UserContext(_httpContextAccessor?.HttpContext);
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedBy = userContext.Name ?? "ANONYMOUS";
                }
                else
                {
                    Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedBy = userContext.Name ?? "ANONYMOUS";
                ((BaseAuditableEntity)entityEntry.Entity).Version = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<UserTaskBoardRole> UserBoardRoles { get; set; }
        public DbSet<TaskBoardColumn> BoardColumns { get; set; }
        public DbSet<TaskCard> TaskCards { get; set; }
    }
}
