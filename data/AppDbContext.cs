using Microsoft.EntityFrameworkCore;
using task_manager_dotnet.Models;

namespace task_manager_dotnet.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("tasks");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(t => t.Description);

            entity.Property(t => t.Status)
                  .HasColumnName("status");

            entity.Property(t => t.CreatedAt)
                  .HasColumnName("created_at");
        });
    }
}
