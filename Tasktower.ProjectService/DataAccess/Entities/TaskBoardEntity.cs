﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasktower.Lib.Aspnetcore.DataAccess.Entities;
using Tasktower.Lib.Aspnetcore.Tools;

namespace Tasktower.ProjectService.DataAccess.Entities
{
    public class TaskBoardEntity : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Columns { get; set; }
        
        public Guid ProjectId { get; set; }
        public virtual ProjectEntity ProjectEntity { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }

        public static void BuildEntity(EntityTypeBuilder<TaskBoardEntity> entityTypeBuilder)
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
                    e => 
                        JsonSerializer.Serialize(e, JsonSerializerUtils.CustomSerializerOptions()), 
                    e => 
                        JsonSerializer.Deserialize<List<string>>(e, JsonSerializerUtils.CustomSerializerOptions()),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToList()))
                .HasDefaultValue(new List<string>());

            entityTypeBuilder.Property(e => e.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            entityTypeBuilder
                .HasOne(e => e.ProjectEntity)
                .WithMany(t => t.TaskBoards)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder
                .HasMany(e => e.Tasks)
                .WithOne(e => e.TaskBoardEntity)
                .HasForeignKey(e => e.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
