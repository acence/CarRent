using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using CarRent.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CarRent.IntegrationTests.Configuration
{
    public static class CarTestSetup
    {
        public static async Task SetupEnvironmentForCarControllerTests(ICarRepository carRepository)
        {
            await carRepository.InsertOrUpdate(x => x.Id == 1, new Car { Id = 1, Make = "Audi", Model = "A4", UniqueId = "C223" }, CancellationToken.None);
            await carRepository.InsertOrUpdate(x => x.Id == 2, new Car { Id = 2, Make = "Citroen", Model = "C-Elysee", UniqueId = "C112" }, CancellationToken.None);
            await carRepository.InsertOrUpdate(x => x.Id == 3, new Car { Id = 3, Make = "Peugeot", Model = "4008", UniqueId = "C357" }, CancellationToken.None);
            await carRepository.InsertOrUpdate(x => x.Id == 4, new Car { Id = 4, Make = "BMW", Model = "320", UniqueId = "C655" }, CancellationToken.None);
            await carRepository.InsertOrUpdate(x => x.Id == 5, new Car { Id = 5, Make = "Audi", Model = "A5", UniqueId = "C965" }, CancellationToken.None);
            await carRepository.InsertOrUpdate(x => x.Id == 6, new Car { Id = 6, Make = "Ferrari", Model = "F40", UniqueId = "C258" }, CancellationToken.None);
        }
    }
}
