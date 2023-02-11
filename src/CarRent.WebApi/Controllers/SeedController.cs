using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CarRent.WebApi.Controllers
{
    [Route("api/v1/seed")]
    [ApiController]
    [ExcludeFromCodeCoverage(Justification = "Used only internally")]
    public class SeedController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRentalRepository _rentalRepository;

        public SeedController(ICarRepository carRepository, IUserRepository userRepository, IRentalRepository rentalRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
            _rentalRepository = rentalRepository;
        }

        [HttpPost]
        public async Task SeedData()
        {
            await _userRepository.InsertOrUpdate(x => x.Id == 1, new User { Id = 1, Name = "Aleksandar Trajkov" });

            await _carRepository.InsertOrUpdate(x => x.Id == 1, new Car { Id = 1, Make = "Audi", Model = "A4", UniqueId = "C223" });
            await _carRepository.InsertOrUpdate(x => x.Id == 2, new Car { Id = 2, Make = "Citroen", Model = "C-Elysee", UniqueId = "C112" });
            await _carRepository.InsertOrUpdate(x => x.Id == 3, new Car { Id = 3, Make = "Peugeot", Model = "4008", UniqueId = "C357" });
            await _carRepository.InsertOrUpdate(x => x.Id == 4, new Car { Id = 4, Make = "BMW", Model = "320", UniqueId = "C655" });
            await _carRepository.InsertOrUpdate(x => x.Id == 5, new Car { Id = 5, Make = "Audi", Model = "A5", UniqueId = "C965" });
            await _carRepository.InsertOrUpdate(x => x.Id == 6, new Car { Id = 6, Make = "Ferrari", Model = "F40", UniqueId = "C258" });

            await _rentalRepository.InsertOrUpdate(x => x.Id == 1, new Rental { Id = 1, CarId = 1, UserId = 1, RentDate = DateOnly.FromDateTime(DateTimeOffset.Now.Date) });
            await _rentalRepository.InsertOrUpdate(x => x.Id == 2, new Rental { Id = 2, CarId = 4, UserId = 1, RentDate = DateOnly.FromDateTime(DateTimeOffset.Now.Date) });
            await _rentalRepository.InsertOrUpdate(x => x.Id == 3, new Rental { Id = 3, CarId = 1, UserId = 1, RentDate = DateOnly.FromDateTime(DateTimeOffset.Now.Date.AddDays(5)) });
            await _rentalRepository.InsertOrUpdate(x => x.Id == 4, new Rental { Id = 4, CarId = 1, UserId = 1, RentDate = DateOnly.FromDateTime(DateTimeOffset.Now.Date.AddDays(-5)) });
        }
    }
}
