using AutoMapper;
using Microsoft.EntityFrameworkCore;

// ReSharper disable MemberCanBePrivate.Global

namespace TaskManager.Services;

public abstract class ServiceBase<TContext> where TContext : DbContext
{
    /// <summary>
    /// App main context
    /// </summary>
    /// <returns></returns>
    protected readonly TContext Context; 

    protected readonly IMapper Mapper;

    protected ServiceBase(TContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
}