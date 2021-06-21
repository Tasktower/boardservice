using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasktower.Lib.Aspnetcore.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Entities
{
    public class ProjectEntity : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<ProjectRoleEntity> ProjectRoles { get; set; }

        public ICollection<TaskBoardEntity> TaskBoards { get; set; }

        public static void BuildEntity(EntityTypeBuilder<ProjectEntity> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("projects");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entityTypeBuilder.HasKey(e => e.Id );

            entityTypeBuilder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired()
                .HasDefaultValue("");

            entityTypeBuilder
                .HasMany(e => e.TaskBoards)
                .WithOne(e => e.ProjectEntity)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entityTypeBuilder
                .HasMany(e => e.ProjectRoles)
                .WithOne(e => e.ProjectEntity)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        public new static IQueryable<ProjectEntity> OrderByQuery(string orderBy, IQueryable<ProjectEntity> queryable)
        {
            return orderBy switch
            {
                "id" => queryable.OrderBy(e => e.Id),
                "id_desc" => queryable.OrderByDescending(e => e.Id),
                "title" => queryable.OrderBy(e => e.Title),
                "title_desc" => queryable.OrderByDescending(e => e.Title),
                "description" => queryable.OrderBy(e => e.Description),
                "description_desc" => queryable.OrderByDescending(e => e.Description),
                _ => AuditableEntity.OrderByQuery(orderBy, queryable)
            };
        }
    }
}
