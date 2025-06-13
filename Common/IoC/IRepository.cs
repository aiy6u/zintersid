using System.Linq.Expressions;

namespace zintersid.Common.IoC
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync<T>(T id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync<T>(T id);
    }
}