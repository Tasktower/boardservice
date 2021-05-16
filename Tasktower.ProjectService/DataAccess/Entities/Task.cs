﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.ProjectService.DataAccess.Entities
{
    public class Task : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        
        public string Column { get; set; }
        public Guid TaskBoardId { get; set; }
        public virtual TaskBoard TaskBoard { get; set; }

        public static void BuildEntity(EntityTypeBuilder<Task> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("tasks");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entityTypeBuilder.HasKey(e => new { e.Id });

            entityTypeBuilder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.TaskDescriptionMarkup)
                .HasColumnName("mk_description")
                .IsRequired()
                .HasDefaultValue("");
            
            entityTypeBuilder.Property(e => e.Column)
                .HasColumnName("column")
                .HasMaxLength(300);

            entityTypeBuilder.Property(e => e.TaskBoardId)
                .HasColumnName("task_board_id");

            entityTypeBuilder
                .HasOne(e => e.TaskBoard)
                .WithMany(c => c.Tasks)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
