using Common.Domain.Specification;
using System.Linq.Expressions;

namespace Common.Domain.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    IReadOnlyList<TEntity> Get();
    (IQueryable<TEntity> data, int count) GetWithSpec(Specification<TEntity> specification);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    TEntity? GetEntityWithSpec(Specification<TEntity> specification);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entity);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    void ExecuteUpdateRange(Expression<Func<TEntity, bool>> filter, Expression<Func<Microsoft.EntityFrameworkCore.Query.SetPropertyCalls<TEntity>, Microsoft.EntityFrameworkCore.Query.SetPropertyCalls<TEntity>>> expression);
}
