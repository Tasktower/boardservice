using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasktower.Lib.Aspnetcore.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Entities
{
    public class UserEntity : AuditableEntity
    {
        public string Id { get; set; }
        public string UserId { get => Id; set => Id = value; }
        public string UserName { get; set; }
        public ICollection<ProjectRoleEntity> ProjectRoles { get; set; }

        public static void BuildEntity(EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            entityTypeBuilder = entityTypeBuilder.ToTable("users");
            BuildAuditableEntity(entityTypeBuilder);

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasMaxLength(100)
                .IsRequired();
            entityTypeBuilder.HasKey(e => e.Id);

            entityTypeBuilder.Ignore(e => e.UserId);
            
            entityTypeBuilder.Property(e => e.UserName)
                .HasColumnName("username")
                .HasMaxLength(100)
                .IsRequired();
            
            entityTypeBuilder
                .HasMany(e => e.ProjectRoles)
                .WithOne(e => e.UserEntity)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}