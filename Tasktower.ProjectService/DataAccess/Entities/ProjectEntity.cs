using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasktower.Lib.Aspnetcore.DataAccess.Entities;
using Tasktower.Lib.Aspnetcore.Paging;
using Tasktower.Lib.Aspnetcore.Paging.Extensions;

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
        
        public static IQueryable<ProjectEntity> OrderByQuery(IQueryable<ProjectEntity> queryable, 
            SortValue sortValue, bool chainOrder = false)
        {
            return sortValue.Field switch
            {
                "id" => queryable.OrderBySortValue(sortValue,e => e.Id, chainOrder),
                "title" => queryable.OrderBySortValue(sortValue,e => e.Title, chainOrder),
                "description" => queryable.OrderBySortValue(sortValue,e => e.Description, chainOrder),
                _ => AuditableEntity.OrderByQuery(queryable, sortValue, chainOrder)
            };
        }
    }
}
