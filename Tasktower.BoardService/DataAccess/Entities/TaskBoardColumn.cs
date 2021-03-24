using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.BoardService.DataAccess.Entities
{
    public class TaskBoardColumn : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public string Name { get; set; }
        public virtual TaskBoard TaskBoard { get; set; }
        public ICollection<TaskCard> TaskCards { get; set; }

        public static void BuildEntity(EntityTypeBuilder<TaskBoardColumn> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("task_board_columns");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            entityTypeBuilder.Property(e => e.TaskBoardId)
                .HasColumnName("task_board_id")
                .IsRequired();


            entityTypeBuilder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder
                .HasKey(e => new { e.Id });

            entityTypeBuilder
                .HasOne(e => e.TaskBoard)
                .WithMany(t => t.TaskBoardColumns)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder
                .HasMany(e => e.TaskCards)
                .WithOne(e => e.TaskBoardColumn)
                .HasForeignKey(e => e.TaskBoardColumnId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
