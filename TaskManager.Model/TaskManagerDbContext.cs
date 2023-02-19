using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Model.Entities;

#pragma warning disable CS8618

namespace TaskManagerApi.Model;

// ReSharper disable once UnusedAutoPropertyAccessor.Local
public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) {}
    
    /// <summary>
    /// Task desc entity 
    /// </summary>
    public DbSet<TaskDescEntity> TaskEntities { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
}