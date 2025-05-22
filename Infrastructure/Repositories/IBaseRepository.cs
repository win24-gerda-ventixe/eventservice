using Infrastructure.Models;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<RepositoryResult> AddAsync(TEntity entity);
    Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();

    // Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    // Task<RepositoryResult> UpdateAsync(TEntity entity);
    // Task<RepositoryResult> DeleteAsync(TEntity entity);
    // Task<RepositoryResult> GetByIdAsync(string id);
    // Task<RepositoryResult> GetByStatusAsync(string status);
}
