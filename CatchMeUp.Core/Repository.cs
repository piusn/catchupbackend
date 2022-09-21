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
    T GetByID(object id);
    Task<Favourite?> GetFavouriteByUserIds(int userId, int memberId);
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

    public virtual async Task Delete(T entityToDelete)
    {
        if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
        await dbContext.SaveChangesAsync();
    }

    public virtual T GetByID(object id)
    {
        return dbSet.Find(id);
    }
    public virtual async Task Insert(T entity)
    {
        dbSet.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public Task<Favourite?> GetFavouriteByUserIds(int userId, int memberId)
    {
        return Task.FromResult(dbContext.Favourites.Where(p => p.UserId.Equals(userId) && p.MemberId.Equals(memberId)).FirstOrDefault());
    }
}