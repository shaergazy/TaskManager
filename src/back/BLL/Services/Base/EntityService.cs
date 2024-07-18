using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Interfaces;
using DAL.EF;
using DAL.Entities.Users;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Base;

public abstract class EntityService<TEntity, TKey> : ContextHasService
    where TEntity : class, IIdHas<TKey>
    where TKey : IEquatable<TKey>
{
    protected EntityService(AppDbContext context) : base(context)
    {
        context.Set<User>();
    }

    public virtual async Task<TEntity> Add<T>(T model)
    {
        var entity = model.Adapt<TEntity>();
        await BeforeAdd(entity);
        await Check(entity);
        await Context.Set<TEntity>().AddAsync(entity);
        await SaveChanges();

        return entity;
    }

    protected virtual Task BeforeAdd(TEntity entity)
    {
        return Task.CompletedTask;
    }

    protected virtual Task Check(TEntity entity)
    {
        return Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<T>> List<T>()
    {
        return await List<T>(Context.Set<TEntity>());
    }

    public async Task<IEnumerable<T>> List<T>(IQueryable<TEntity> query)
    {
        return await query
            .AsNoTracking()
            .ProjectToType<T>()
            .ToListAsync();
    }

    public virtual async Task<(IEnumerable<T> Data, int Total)> Page<T>(int number, int size)
    {
        return await Page<T>(Context.Set<TEntity>(), number, size);
    }

    public async Task<(IEnumerable<T> Data, int Total)> Page<T>(IQueryable<TEntity> query, int number, int size)
    {
        return (await query
            .AsNoTracking()
            .Page(number, size)
            .ProjectToType<T>()
            .ToListAsync(), await Total(query, size));
    }

    protected async Task<int> Total(IQueryable<TEntity> query, int size)
    {
        var count = await query
            .CountAsync();
        return count == 0
            ? 1
            : count / size + (count % size > 0 ? 1 : 0);
    }

    public virtual async Task<T> ById<T>(TKey id) where T : class, IIdHas<TKey>
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .ProjectToType<T>()
            .ById(id);
    }

    public virtual async Task Edit<T>(T model) where T : IIdHas<TKey>
    {
        var entity = await Context.Set<TEntity>().ById(model.Id);
        model.Adapt(entity);
        await Check(entity);

        await SaveChanges();
    }

    public virtual async Task Delete(TKey id)
    {
        Context.Set<TEntity>().Remove(await Context.Set<TEntity>().ById(id));
        await SaveChanges();
    }
}
