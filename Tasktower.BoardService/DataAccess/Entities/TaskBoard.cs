using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.BoardService.DataAccess.Entities
{
    public class TaskBoard : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Columns { get; set; }
        
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }

        public static void BuildEntity(EntityTypeBuilder<TaskBoard> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("task_boards");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entityTypeBuilder.HasKey(e => e.Id);

            entityTypeBuilder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();
            
            entityTypeBuilder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired()
                .HasDefaultValue("");

            entityTypeBuilder.Property(e => e.Columns)
                .HasColumnName("columns")
                .IsRequired()
                .HasConversion(
                    e => JsonSerializer.Serialize(e, null),
                    e => JsonSerializer.Deserialize<List<String>>(e, null),
                    new ValueComparer<List<String>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()))
                .HasDefaultValue(new List<String>());

            entityTypeBuilder.Property(e => e.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            entityTypeBuilder
                .HasOne(e => e.Project)
                .WithMany(t => t.TaskBoards)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder
                .HasMany(e => e.Tasks)
                .WithOne(e => e.TaskBoard)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
