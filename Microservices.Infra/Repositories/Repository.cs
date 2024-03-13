using Microservices.Domain.Entities;
using Microservices.Domain.Repositories;
using Microservices.Infra.Data;
using Microservices.Infra.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Microservices.Infra.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    public DbSet<T> dbSet { get; set; }
    private AppDbContext _db { get; set; }

    public Repository(AppDbContext db)
    {
        _db = db;
        dbSet = db.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }
        catch (Exception e)
        {
            throw new RepositoryException(e.Message, entity.GetType().Name);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            dbSet.Remove(entity);
            return await SaveAsync();
        }
        catch (Exception e)
        {
            throw new RepositoryException(e.Message, "DeleteAsync");
        }     
    }

    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }

        var entity = await query.FirstOrDefaultAsync(_ => _.Id == id);
        if (entity == null) throw new RepositoryException($"Not found any ENTITY with this ID - {id}", "GetByIdAsync");
        return entity;
    }

    public async Task<bool> SaveAsync() => await _db.SaveChangesAsync() == 1 ? true : false; 
}
