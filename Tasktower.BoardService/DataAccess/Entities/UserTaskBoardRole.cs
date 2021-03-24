using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.BoardService.DataAccess.Entities
{
    public class UserTaskBoardRole : BaseAuditableEntity
    {
        public enum BoardRole { OWNER, BOARD_EDITOR, BOARD_READER }
        public Guid TaskBoardId { get; set; }

        public virtual TaskBoard TaskBoard { get; set; }

        public string UserId { get; set; }

        public BoardRole Role { get; set; }

        public static void BuildEntity(EntityTypeBuilder<UserTaskBoardRole > entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("user_task_board_roles");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.TaskBoardId)
                .HasColumnName("task_board_id")
                .IsRequired();

            entityTypeBuilder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.Role)
                .HasConversion(
                    r => r.ToString(),
                    s => Enum.Parse<UserTaskBoardRole.BoardRole>(s))
                .HasColumnName("role")
                .HasMaxLength(20)
                .IsRequired();

            entityTypeBuilder
                .HasKey(e => new { e.TaskBoardId, e.UserId });

            entityTypeBuilder
                .HasOne(e => e.TaskBoard)
                .WithMany(t => t.UserBoardRoles)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
