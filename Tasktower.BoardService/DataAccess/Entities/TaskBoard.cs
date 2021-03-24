using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.BoardService.DataAccess.Entities
{
    public class TaskBoard : BaseAuditableEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<UserTaskBoardRole> UserBoardRoles { get; set; }

        public ICollection<TaskBoardColumn> TaskBoardColumns { get; set; }

        public static void BuildEntity(EntityTypeBuilder<TaskBoard> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("task_boards");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            entityTypeBuilder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired()
                .HasDefaultValue("");

            entityTypeBuilder.HasKey(e => new { e.Id });
            
            entityTypeBuilder
                .HasMany(e => e.TaskBoardColumns)
                .WithOne(e => e.TaskBoard)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entityTypeBuilder
                .HasMany(e => e.UserBoardRoles)
                .WithOne(e => e.TaskBoard)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
