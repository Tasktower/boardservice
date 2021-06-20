using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasktower.ProjectService.DataAccess.Entities.Base
{
    public abstract class AuditableEntity
    {
        public byte[] Version { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static void BuildAuditableEntity<T>(EntityTypeBuilder<T> entityTypeBuilder) 
            where T : AuditableEntity
        {
            entityTypeBuilder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();
            entityTypeBuilder.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(300)
                .IsRequired();
            entityTypeBuilder.Property(e => e.ModifiedAt)
                .HasColumnName("modified_at")
                .IsRequired();
            entityTypeBuilder.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(300)
                .IsRequired();
            entityTypeBuilder.Property(e => e.Version)
                .HasColumnName("version")
                .IsRowVersion();
        }

        public static IQueryable<ProjectEntity> OrderByQuery(string orderBy, IQueryable<ProjectEntity> queryable)
        {
            return orderBy switch
            {
                "createdBy" => queryable.OrderBy(e => e.CreatedBy),
                "createdBy_desc" => queryable.OrderByDescending(e => e.CreatedBy),
                "createdAt" => queryable.OrderBy(e => e.CreatedAt.Ticks),
                "createdAt_desc" => queryable.OrderByDescending(e => e.CreatedAt),
                "modifiedBy" => queryable.OrderBy(e => e.ModifiedBy),
                "modifiedBy_desc" => queryable.OrderByDescending(e => e.ModifiedBy),
                "modifiedAt" => queryable.OrderBy(e => e.ModifiedAt),
                "modifiedAt_desc" => queryable.OrderByDescending(p => p.ModifiedAt),
                _ => queryable
            };

        }
    }
}
