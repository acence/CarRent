using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FitnessApp.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRent.Database.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(IDatabaseContext context, ILogger<BaseRepository<Car>> logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Car>> GetAllAsync(string? make, string? model, string? uniqueId)
        {
            var queryable = Select();

            var test = await Select().ToListAsync();

            if (make != null) {  queryable = queryable.Where(x => x.Make.Equals(make, StringComparison.InvariantCultureIgnoreCase)); }
            if (model != null) { queryable = queryable.Where(x => x.Model.Equals(model, StringComparison.InvariantCultureIgnoreCase)); }
            if (uniqueId != null) { queryable = queryable.Where(x => x.UniqueId.StartsWith(uniqueId, StringComparison.InvariantCultureIgnoreCase)); }

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTimeOffset from)
        {
            return await Select()
                .Where(x => !x.Rentals.Any(y => y.From >= from))
                .ToListAsync();
        }
        public async Task<bool> DoesCarExistAsync(int carId)
        {
            return await Select()
                .Where(x => x.Id == carId)
                .AnyAsync();
        }

        public async Task<bool> IsCarUniqueIdInUseAsync(string uniqueId)
        {
            return await Select()
                .Where(x => x.UniqueId == uniqueId)
                .AnyAsync();
        }
    }
}
