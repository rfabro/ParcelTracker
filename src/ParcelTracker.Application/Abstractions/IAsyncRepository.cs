using System.Linq.Expressions;
using ParcelTracker.Core.Abstractions;

namespace ParcelTracker.Application.Abstractions;

public interface IAsyncRepository<T> where T : Entity
{
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);
}