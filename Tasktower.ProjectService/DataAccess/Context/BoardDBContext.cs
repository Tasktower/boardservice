using Microsoft.EntityFrameworkCore;
using Tasktower.Lib.Aspnetcore.DataAccess.Context;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;

namespace Tasktower.ProjectService.DataAccess.Context
{
    public class BoardDBContext : BaseEfCoreDbContext
    {
        public BoardDBContext(DbContextOptions<BoardDBContext> options, IUserContextService userContextService) :
            base(options, userContextService) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.ProjectEntity>(Entities.ProjectEntity.BuildEntity);
            modelBuilder.Entity<Entities.ProjectRoleEntity>(Entities.ProjectRoleEntity.BuildEntity);
            modelBuilder.Entity<Entities.TaskBoardEntity>(Entities.TaskBoardEntity.BuildEntity);
            modelBuilder.Entity<Entities.TaskEntity>(Entities.TaskEntity.BuildEntity);
        }
        
        public DbSet<Entities.ProjectEntity> Projects { get; set; }
        public DbSet<Entities.ProjectRoleEntity> ProjectRoles { get; set; }
        public DbSet<Entities.TaskBoardEntity> TaskBoards { get; set; }
        public DbSet<Entities.TaskEntity> Tasks { get; set; }
    }
}
