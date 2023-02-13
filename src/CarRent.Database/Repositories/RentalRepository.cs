using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FitnessApp.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Database.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(IDatabaseContext context, ILogger<BaseRepository<Rental>> logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(DateTimeOffset from, int userId, CancellationToken cancellationToken)
        {
            return await Select()
                .Where(x => x.UserId == userId && x.From >= from)
                .Include(x => x.User)
                .Include(x => x.Car)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> DoesRentalExistForCarAsync(DateTimeOffset from, DateTimeOffset to, int carId, CancellationToken cancellationToken)
        {
            return await Select()
                .Where(x => x.CarId == carId && x.From >= from && x.To <= to)
                .AnyAsync(cancellationToken);
        }

        public async Task<Rental> GetByIdWithParentsAsync(int rentalId, CancellationToken cancellationToken)
        {
            return await Select()
                .Include(x => x.Car)
                .Include(x => x.User)
                .FirstAsync(x => x.Id == rentalId, cancellationToken);
        }
    }
}
