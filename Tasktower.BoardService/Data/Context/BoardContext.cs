using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Entities;
using Tasktower.BoardService.Security;

namespace Tasktower.BoardService.Data.Context
{
    public class BoardContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BoardContext(DbContextOptions<BoardContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static void BuildAuditableEntity<T>(EntityTypeBuilder<T> entityTypeBuilder) 
            where T : AbstractAuditableEntity
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
                .IsRequired()
                .IsRowVersion()
                .HasDefaultValue(0);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskBoard>(entityTypeBuilder =>
            {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("task_boards");
                BuildAuditableEntity(entityTypeBuilder);

                entityTypeBuilder.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired()
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entityTypeBuilder.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsRequired();

                entityTypeBuilder.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsRequired()
                    .HasDefaultValue("");

                entityTypeBuilder.HasKey(e => new { e.Id });
            });

            modelBuilder.Entity<UserBoardRole>(entityTypeBuilder => {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("user_board_roles");
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
                        s => Enum.Parse<UserBoardRole.BoardRole>(s))
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .IsRequired();

                entityTypeBuilder
                    .HasKey(e => new { e.TaskBoardId, e.UserId });

                entityTypeBuilder
                    .HasOne(e => e.TaskBoard)
                    .WithMany(t => t.UserBoardRole)
                    .HasForeignKey(e => e.TaskBoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BoardColumn>(entityTypeBuilder => {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("board_columns");
                BuildAuditableEntity(entityTypeBuilder);

                entityTypeBuilder.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired()
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entityTypeBuilder.Property(e => e.TaskBoardId)
                    .HasColumnName("task_board_id")
                    .IsRequired();


                entityTypeBuilder.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entityTypeBuilder
                    .HasKey(e => new { e.Id });

                entityTypeBuilder
                    .HasOne(e => e.TaskBoard)
                    .WithMany(t => t.BoardColumns)
                    .HasForeignKey(e => e.TaskBoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskCard>(entityTypeBuilder =>
            {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("task_cards");
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

                entityTypeBuilder.Property(e => e.BoardColumnId)
                    .HasColumnName("board_column_id");

                entityTypeBuilder
                    .HasOne(e => e.BoardColumn)
                    .WithMany(c => c.TaskCards)
                    .HasForeignKey(e => e.BoardColumnId)
                    .OnDelete(DeleteBehavior.Cascade);

                entityTypeBuilder
                    .HasKey(e => new { e.Id });
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AbstractAuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                UserContext userContext = new UserContext(_httpContextAccessor?.HttpContext?.User);
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((AbstractAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((AbstractAuditableEntity)entityEntry.Entity).CreatedBy = userContext.Name ?? "ANONYMOUS";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((AbstractAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((AbstractAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((AbstractAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((AbstractAuditableEntity)entityEntry.Entity).ModifiedBy = userContext.Name ?? "ANONYMOUS";
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<UserBoardRole> UserBoardRoles { get; set; }
        public DbSet<BoardColumn> BoardColumns { get; set; }
        public DbSet<TaskCard> TaskCards { get; set; }
    }
}
