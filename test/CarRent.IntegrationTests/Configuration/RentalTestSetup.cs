using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using CarRent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.IntegrationTests.Configuration
{
    public static class RentalTestSetup
    {
        public static async Task SetupEnvironmentForRentalControllerTests(IUserRepository userRepository, ICarRepository carRepository, IRentalRepository rentalRepository)
        {
            await userRepository.InsertOrUpdate(x => x.Id == 1, new User { Id = 1, Name = "Aleksandar Trajkov" });

            await carRepository.InsertOrUpdate(x => x.Id == 1, new Car { Id = 1, Make = "Audi", Model = "A4", UniqueId = "C223" });
            await carRepository.InsertOrUpdate(x => x.Id == 2, new Car { Id = 2, Make = "Citroen", Model = "C-Elysee", UniqueId = "C112" });
            await carRepository.InsertOrUpdate(x => x.Id == 3, new Car { Id = 3, Make = "Peugeot", Model = "4008", UniqueId = "C357" });
            await carRepository.InsertOrUpdate(x => x.Id == 4, new Car { Id = 4, Make = "BMW", Model = "320", UniqueId = "C655" });
            await carRepository.InsertOrUpdate(x => x.Id == 5, new Car { Id = 5, Make = "Audi", Model = "A5", UniqueId = "C965" });
            await carRepository.InsertOrUpdate(x => x.Id == 6, new Car { Id = 6, Make = "Ferrari", Model = "F40", UniqueId = "C258" });

            await rentalRepository.InsertOrUpdate(x => x.Id == 1, new Rental { Id = 1, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddHours(8), To = DateTimeOffset.Now.Date.AddHours(9) });
            await rentalRepository.InsertOrUpdate(x => x.Id == 2, new Rental { Id = 2, CarId = 4, UserId = 1, From = DateTimeOffset.Now.Date.AddHours(15), To = DateTimeOffset.Now.Date.AddHours(15).AddMinutes(45) });
            await rentalRepository.InsertOrUpdate(x => x.Id == 3, new Rental { Id = 3, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddDays(5), To = DateTimeOffset.Now.Date.AddDays(5).AddHours(1) });
            await rentalRepository.InsertOrUpdate(x => x.Id == 4, new Rental { Id = 4, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddDays(-5), To = DateTimeOffset.Now.Date.AddDays(-5).AddHours(1) });
        }
    }
}
