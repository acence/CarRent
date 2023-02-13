using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.IntegrationTests.Configuration;
using CarRent.IntegrationTests.Helpers;
using CarRent.IntegrationTests.TestData.Rentals;
using CarRent.WebApi.ResponseModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace CarRent.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class RentalControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;

        public RentalControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            _userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;
            _carRepository = scope.ServiceProvider.GetService<ICarRepository>()!;
            _rentalRepository = scope.ServiceProvider.GetService<IRentalRepository>()!;
            RentalTestSetup.SetupEnvironmentForRentalControllerTests(_userRepository, _carRepository, _rentalRepository).GetAwaiter().GetResult();
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_UpcomingRentals_ShouldReturnAListOfReservationsOnDate(int userId)
        {
            // Arrange
            var client = _factory.CreateClient();
            var from = DateTimeOffset.Now.Date.AddHours(15);

            // Act 
            var response = await client.GetAsync($"/api/v1/rentals/{userId}/upcoming?from={from}");
            var result = await SerializationHelper.GetDeserializedValue<IEnumerable<RentalResponse>>(response);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Get_AvailableCars_ShouldReturnAListOfCarsOnDate()
        {
            // Arrange
            var client = _factory.CreateClient();
            var from = DateTimeOffset.Now.Date.AddHours(15);

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CarResponse>>($"/api/v1/rentals/available-cars?from={from}");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4);
        }

        [Theory]
        [ClassData(typeof(CreateRentalData))]
        public async Task Create_RentalWithValidData_ReturnsCreatedCar(CreateRental.Command rental)
        {// Arrange
            var client = _factory.CreateClient();

            // Act 
            var content = JsonContent.Create(rental, options: SerializationHelper.Options);
            var result = await client.PostAsync($"/api/v1/rentals", content);

            var rentalResponse = await SerializationHelper.GetDeserializedValue<RentalResponse>(result);

            // Assert
            rentalResponse.Should().NotBeNull();
            rentalResponse.Id.Should().BeGreaterThan(0);
            rentalResponse.User.Id.Should().Be(rental.UserId);
            rentalResponse.Car.Id.Should().Be(rental.CarId);
        }
    }
}
