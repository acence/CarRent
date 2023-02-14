using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CarRent.WebApi.Controllers
{
    /// <summary>
    /// Rest API controller used for initiall seed for manual testing
    /// </summary>
    [ApiVersionNeutral]
    [Route("api/seed")]
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

        /// <summary>
        /// Seeds data to the database
        /// </summary>
        /// <response code="200">Seed succeeded</response>
        /// <response code="500">Seed failed, most likely due to db issue</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task SeedData()
        {
            await _userRepository.InsertOrUpdate(x => x.Id == 1, new User { Id = 1, Name = "Aleksandar Trajkov" }, CancellationToken.None);

            await _carRepository.InsertOrUpdate(x => x.Id == 1, new Car { Id = 1, Make = "Audi", Model = "A4", UniqueId = "C223" }, CancellationToken.None);
            await _carRepository.InsertOrUpdate(x => x.Id == 2, new Car { Id = 2, Make = "Citroen", Model = "C-Elysee", UniqueId = "C112" }, CancellationToken.None);
            await _carRepository.InsertOrUpdate(x => x.Id == 3, new Car { Id = 3, Make = "Peugeot", Model = "4008", UniqueId = "C357" }, CancellationToken.None);
            await _carRepository.InsertOrUpdate(x => x.Id == 4, new Car { Id = 4, Make = "BMW", Model = "320", UniqueId = "C655" }, CancellationToken.None);
            await _carRepository.InsertOrUpdate(x => x.Id == 5, new Car { Id = 5, Make = "Audi", Model = "A5", UniqueId = "C965" }, CancellationToken.None);
            await _carRepository.InsertOrUpdate(x => x.Id == 6, new Car { Id = 6, Make = "Ferrari", Model = "F40", UniqueId = "C258" }, CancellationToken.None);

            await _rentalRepository.InsertOrUpdate(x => x.Id == 1, new Rental { Id = 1, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddHours(8), To = DateTimeOffset.Now.Date.AddHours(9) }, CancellationToken.None);
            await _rentalRepository.InsertOrUpdate(x => x.Id == 2, new Rental { Id = 2, CarId = 4, UserId = 1, From = DateTimeOffset.Now.Date.AddHours(15), To = DateTimeOffset.Now.Date.AddHours(15).AddMinutes(45) }, CancellationToken.None);
            await _rentalRepository.InsertOrUpdate(x => x.Id == 3, new Rental { Id = 3, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddDays(5), To = DateTimeOffset.Now.Date.AddDays(5).AddHours(1) }, CancellationToken.None);
            await _rentalRepository.InsertOrUpdate(x => x.Id == 4, new Rental { Id = 4, CarId = 1, UserId = 1, From = DateTimeOffset.Now.Date.AddDays(-5), To = DateTimeOffset.Now.Date.AddDays(-5).AddHours(1) }, CancellationToken.None);
        }
    }
}
