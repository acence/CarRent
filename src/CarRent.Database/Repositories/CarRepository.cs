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

            if (make != null) {  queryable = queryable.Where(x => x.Make == make); }
            if (model != null) { queryable = queryable.Where(x => x.Model == model); }
            if (uniqueId != null) { queryable = queryable.Where(x => x.Make.Contains(uniqueId)); }

            return await queryable.ToListAsync();
        }
    }
}
