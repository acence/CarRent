using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Base;
using CarRent.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace FitnessApp.Database.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        public readonly IDatabaseContext _context;
        private readonly ILogger<BaseRepository<T>> _logger;
        private DbSet<T>? _entities;

        public BaseRepository(IDatabaseContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Select()
        {
            return Entities.AsNoTracking();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<int> Insert(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            (_context as DbContext)!.Entry(entity).State = EntityState.Added;
            
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            (_context as DbContext)!.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> InsertOrUpdate(Expression<Func<T, bool>> comparer, T entity)
        {
            if (!Entities.Any(comparer))
            {
                return await Insert(entity);
            }
            else
            {
                return await Update(entity);
            }
        }

        public virtual async Task<Int32> Delete(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Entities.Remove(entity);

            return await _context.SaveChangesAsync();
        }
    }
}
