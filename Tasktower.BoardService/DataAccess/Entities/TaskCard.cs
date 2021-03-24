using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.BoardService.DataAccess.Entities
{
    public class TaskCard : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        public Guid TaskBoardColumnId { get; set; }
        public virtual TaskBoardColumn TaskBoardColumn { get; set; }

        public static void BuildEntity(EntityTypeBuilder<TaskCard> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("task_cards");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            entityTypeBuilder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.TaskDescriptionMarkup)
                .HasColumnName("mk_description")
                .IsRequired()
                .HasDefaultValue("");

            entityTypeBuilder.Property(e => e.TaskBoardColumnId)
                .HasColumnName("board_column_id");

            entityTypeBuilder
                .HasOne(e => e.TaskBoardColumn)
                .WithMany(c => c.TaskCards)
                .HasForeignKey(e => e.TaskBoardColumnId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder
                .HasKey(e => new { e.Id });
        }
    }
}
