using CarRent.Database.Interfaces.Base;
using CarRent.Domain;

namespace CarRent.Database.Interfaces.Repositories
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllAsync(string? make, string? model, string? uniqueId);
    }
}
