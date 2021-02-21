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
    public class BoardDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BoardDBContext(DbContextOptions<BoardDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected static void BuildAuditableEntity<T>(EntityTypeBuilder<T> entityTypeBuilder) 
            where T : BaseAuditableEntity
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
                .IsConcurrencyToken();
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

            modelBuilder.Entity<UserTaskBoardRole>(entityTypeBuilder => {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("user_task_board_roles");
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
                    .WithMany(t => t.UserBoardRole)
                    .HasForeignKey(e => e.TaskBoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskBoardColumn>(entityTypeBuilder => {
                entityTypeBuilder = entityTypeBuilder
                    .ToTable("task_board_columns");
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
                    .WithMany(t => t.TaskBoardColumns)
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
                    .HasOne(e => e.TaskBoardColumn)
                    .WithMany(c => c.TaskCards)
                    .HasForeignKey(e => e.BoardColumnId)
                    .OnDelete(DeleteBehavior.Cascade);

                entityTypeBuilder
                    .HasKey(e => new { e.Id });
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                UserContext userContext = new UserContext(_httpContextAccessor?.HttpContext);
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedBy = userContext.Name ?? "ANONYMOUS";
                }
                else
                {
                    Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedBy = userContext.Name ?? "ANONYMOUS";
                ((BaseAuditableEntity)entityEntry.Entity).Version = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<UserTaskBoardRole> UserBoardRoles { get; set; }
        public DbSet<TaskBoardColumn> BoardColumns { get; set; }
        public DbSet<TaskCard> TaskCards { get; set; }
    }
}
