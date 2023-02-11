using CarRent.Database.Interfaces.Base;
using CarRent.Domain;

namespace CarRent.Database.Interfaces.Repositories
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllAsync(string? make, string? model, string? uniqueId);
        Task<IEnumerable<Car>> GetAvailableCarsAsync(DateOnly date);
        Task<bool> DoesCarExistAsync(int carId);
        Task<bool> IsCarUniqueIdInUseAsync(string uniqueId);
    }
}
