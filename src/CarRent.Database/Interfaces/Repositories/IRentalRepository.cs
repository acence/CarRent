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
        Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(DateOnly from, int userId);
        Task<bool> DoesRentalExistForCarAsync(DateOnly date, int carId);
    }
}
