using CarRent.Database.Interfaces.Base;
using CarRent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Database.Interfaces.Repositories
{
    public interface IRentalRepository : IBaseRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(DateTimeOffset from, int userId);
        Task<bool> DoesRentalExistForCarAsync(DateTimeOffset from, DateTimeOffset to, int carId);
        Task<Rental> GetByIdWithParentsAsync(int rentalId);
    }
}
