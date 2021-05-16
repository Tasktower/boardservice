using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.ProjectService.DataAccess.Entities
{
    public class ProjectRole : BaseAuditableEntity
    {
        public enum ProjectRoleValue { OWNER, BOARD_EDITOR, BOARD_READER }
        
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ProjectRoleValue Role { get; set; }
        
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public static void BuildEntity(EntityTypeBuilder<ProjectRole > entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("project_roles");
            BuildAuditableEntity(entityTypeBuilder);
            
            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entityTypeBuilder.HasKey(e => e.Id);

            entityTypeBuilder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.Role)
                .HasConversion(
                    r => r.ToString(),
                    s => Enum.Parse<ProjectRoleValue>(s))
                .HasColumnName("role")
                .HasMaxLength(20)
                .IsRequired();

            entityTypeBuilder.Property(e => e.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            entityTypeBuilder
                .HasOne(e => e.Project)
                .WithMany(t => t.ProjectRoles)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder
                .HasCheckConstraint("unique_project_roles", "UNIQUE ([project_id], [user_id])");
        }

    }
}
