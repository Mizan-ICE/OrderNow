using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure.Repositories;
public class Repository<T> : IRepository<T> where T : class,IEntity,new()
{
    private readonly DbContext _dbcontext;
    public Repository(DbContext dbContext)
    {
      _dbcontext = dbContext;  
    }

    public async Task AddAsync(T entity)
    {

        await _dbcontext.Set<T>().AddAsync(entity);
       
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbcontext.Set<T>().FirstOrDefaultAsync(n => n.Id==id);
        EntityEntry entityEntry = _dbcontext.Entry<T>(entity);
        entityEntry.State = EntityState.Deleted;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbcontext.Set<T>().ToListAsync();

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbcontext.Set<T>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id) => await _dbcontext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

    public async Task UpdateAsync( T entity)
    {
        await Task.Run(() =>
        {
            EntityEntry entityEntry = _dbcontext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        });


    }
}
