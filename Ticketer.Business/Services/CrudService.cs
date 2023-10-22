using Microsoft.EntityFrameworkCore;
using Ticketer.Business.Models;

namespace Ticketer.Business.Services;

public abstract class BaseService<T> where T : class, IModel
{
    protected readonly TicketerDbContext DbContext;

    protected BaseService(TicketerDbContext context)
    {
        DbContext = context;
    }

    protected virtual async Task<T> AddAsync(T entity)
    {
        var entityEntry = await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    protected virtual async Task<T> UpdateAsync(T entity)
    {
        var entityEntry =  DbContext.Update(entity);
        await DbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
}