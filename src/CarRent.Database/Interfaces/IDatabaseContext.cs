using CarRent.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Database.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<T> Set<T>() where T : BaseModel;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
