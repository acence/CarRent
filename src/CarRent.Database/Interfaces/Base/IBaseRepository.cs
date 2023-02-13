using System.Linq.Expressions;

namespace CarRent.Database.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> Select();
        Task<T?> GetById(int id, CancellationToken cancellationToken);
        Task<int> Insert(T entity, CancellationToken cancellationToken);
        Task<int> Update(T entity, CancellationToken cancellationToken);
        Task<int> InsertOrUpdate(Expression<Func<T, bool>> comparer, T entity, CancellationToken cancellationToken);
        Task<int> Delete(T entity, CancellationToken cancellationToken);
    }
}
