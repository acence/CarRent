using CarRent.Database.Interfaces.Base;
using CarRent.Domain;

namespace CarRent.Database.Interfaces.Repositories
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllAsync(string? make, string? model, string? uniqueId, CancellationToken cancellationToken);
        Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTimeOffset from, DateTimeOffset? to, CancellationToken cancellationToken);
        Task<bool> DoesCarExistAsync(int carId, CancellationToken cancellationToken);
        Task<bool> IsCarUniqueIdInUseAsync(string uniqueId, CancellationToken cancellationToken);
    }
}
