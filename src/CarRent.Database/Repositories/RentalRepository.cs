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

        public async Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(DateOnly from, int userId)
        {
            return await Select()
                .Where(x => x.RentDate >= from && x.UserId == userId)
                .Include(x => x.User)
                .Include(x => x.Car)
                .ToListAsync();
        }

        public async Task<bool> DoesRentalExistForCarAsync(DateOnly date, int carId)
        {
            return await Select()
                .Where(x => x.CarId == carId && x.RentDate == date)
                .AnyAsync();
        }

        public async Task<Rental> GetByIdWithParentsAsync(int rentalId)
        {
            return await Select()
                .Include(x => x.Car)
                .Include(x => x.User)
                .FirstAsync(x => x.Id == rentalId);
        }
    }
}
