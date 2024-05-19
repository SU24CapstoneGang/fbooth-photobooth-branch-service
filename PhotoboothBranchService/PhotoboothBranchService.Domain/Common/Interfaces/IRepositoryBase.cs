using System.Linq.Expressions;

namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IRepositoryBase<TEntity>
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Guid> AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}
