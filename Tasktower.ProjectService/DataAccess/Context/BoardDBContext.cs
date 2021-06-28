﻿using Microsoft.EntityFrameworkCore;
using Tasktower.Lib.Aspnetcore.DataAccess.Context;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.Lib.Aspnetcore.Services.Contexts;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Context
{
    public class BoardDBContext : BaseEfCoreDbContext
    {
        public BoardDBContext(DbContextOptions<BoardDBContext> options, IUserContext userContext) :
            base(options, userContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(UserEntity.BuildEntity);
            modelBuilder.Entity<ProjectEntity>(ProjectEntity.BuildEntity);
            modelBuilder.Entity<ProjectRoleEntity>(ProjectRoleEntity.BuildEntity);
            modelBuilder.Entity<TaskBoardEntity>(TaskBoardEntity.BuildEntity);
            modelBuilder.Entity<TaskEntity>(TaskEntity.BuildEntity);
        }
        
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectRoleEntity> ProjectRoles { get; set; }
        public DbSet<TaskBoardEntity> TaskBoards { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
