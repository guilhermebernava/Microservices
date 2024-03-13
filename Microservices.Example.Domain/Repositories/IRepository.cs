using Microservices.Domain.Entities;
using System.Linq.Expressions;

namespace Microservices.Domain.Repositories;
public interface IRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveAsync();
}
