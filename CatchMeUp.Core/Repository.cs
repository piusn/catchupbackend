using CatchMeUp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatchMeUp.Core;

public interface IRepository<T>
{
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    Task Delete(T entityToDelete);
    Task Insert(T entity);
    Task Update(T entityToUpdate);
    Task<Favourite?> GetFavouriteByUserIds(int userId, int memberId);
    Task<T> GetByID(int id);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CatchMeUpDbContext dbContext;
    private DbSet<T> dbSet;

    public Repository(CatchMeUpDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual Task Delete(T entityToDelete)
    {
        if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
        return Task.CompletedTask;
    }

    public virtual async Task<T> GetByID(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual Task Insert(T entity)
    {
        dbSet.Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task<Favourite?> GetFavouriteByUserIds(int userId, int memberId)
    {
        return Task.FromResult(dbContext.Favourites.Where(p => p.UserId.Equals(userId) && p.MemberId.Equals(memberId)).FirstOrDefault());
    }
}