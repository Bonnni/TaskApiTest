using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerApi.Model;

namespace TaskManager.Services.Extansions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtansions
{
    /// <summary>
    /// Inject main context into container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext, TaskManagerDbContext>(option =>
        {
            option.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(TaskManagerDbContext).Assembly.GetName().Name);
            });
        });
    }
    
    /// <summary>
    /// Injects mapper into container
    /// </summary>
    public static void AddAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    /// <summary>
    /// Injects query handlers into container
    /// </summary>
    public static void AddRequestHandlers(this IServiceCollection services)
        => services.AddDependenciesAccembly(ServiceLifetime.Scoped, typeof(IRequestHandler<,>), Assembly.GetExecutingAssembly());

    /// <summary>
    /// Inject types from assembly into container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="lifetime"></param>
    /// <param name="type"></param>
    /// <param name="assembly"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static void AddDependenciesAccembly(this IServiceCollection services, ServiceLifetime lifetime, Type type, Assembly assembly)
    {
        var implementations = GetAllImptementationOpenInterfaceType(type, assembly);
        foreach (var handlerType in implementations)
        {
            var interfaceTypes = handlerType.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.GetGenericTypeDefinition() != type) continue;

                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceType, handlerType);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceType, handlerType);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceType, handlerType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                }
            }
        }
    }

    private static IEnumerable<Type> GetAllImptementationOpenInterfaceType(Type openInterfaceType, Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces()
                .Any(type => type.IsGenericType && type.GetGenericTypeDefinition() == openInterfaceType));
    }
}