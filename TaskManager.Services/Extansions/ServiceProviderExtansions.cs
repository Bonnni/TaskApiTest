using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerApi.Model;

namespace TaskManager.Services.Extansions;

/// <summary>
/// Extensions for <see cref="IServiceProvider"/>
/// </summary>
public static class ServiceProviderExtansions
{
    /// <summary>
    /// Migrates context with data seeding
    /// </summary>
    public static async Task MigrateDbAsync(this IServiceProvider services)
    {
        var context = services.GetRequiredService<TaskManagerDbContext>();
        
        if (context.Database.IsSqlite())
        {
            await context.Database.MigrateAsync();
        }
    }
}