namespace CarRent.Database.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> Select();
        Task<T?> GetById(int id);
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
