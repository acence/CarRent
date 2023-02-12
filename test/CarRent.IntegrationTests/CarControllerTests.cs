using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.IntegrationTests.TestData.Cars;
using CarRent.IntegrationTests.Configuration;
using CarRent.IntegrationTests.Helpers;
using CarRent.WebApi.ResponseModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarRent.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private ICarRepository _carRepository;

        public CarControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            _carRepository = scope.ServiceProvider.GetService<ICarRepository>()!;
            CarTestSetup.SetupEnvironmentForCarControllerTests(_carRepository).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task Get_CarList_ReturnsAllCars()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CarResponse>>("/api/v1/cars");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(6);
        }
        [Theory]
        [InlineData("/api/v1/cars?make=audi", 2)]
        [InlineData("/api/v1/cars?model=F40", 1)]
        [InlineData("/api/v1/cars?uniqueId=C2", 2)]
        [InlineData("/api/v1/cars?model=A4&uniqueId=C1", 0)]
        public async Task Get_CarListWithFilter_ReturnsFilteredCars(string url, int count)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CarResponse>>(url);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(count);
        }

        [Theory]
        [ClassData(typeof(CreateCarData))]
        public async Task Create_CarWithValidData_ReturnsCreatedCar(CreateNewCar.Command car)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var response = await client.PostAsJsonAsync("/api/v1/cars", car);
            var carResponse = await SerializationHelper.GetDeserializedValue<CarResponse>(response);

            // Assert
            carResponse.Should().NotBeNull();
            carResponse.Id.Should().Be(7);

            _carRepository.Select().ToList().Should().HaveCount(7);
            var dbCar = await _carRepository.GetById(carResponse.Id);
            dbCar.Should().BeEquivalentTo(car);
        }

        [Theory]
        [ClassData(typeof(UpdateCarData))]
        public async Task Update_CarWithValidData_ReturnsUpdatedCar(UpdateCar.Command car)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var response = await client.PutAsJsonAsync($"/api/v1/cars/{car.Id}", car);
            var carResponse = await SerializationHelper.GetDeserializedValue<CarResponse>(response);

            // Assert
            carResponse.Should().NotBeNull();
            carResponse.Id.Should().Be(4);

            _carRepository.Select().ToList().Should().HaveCount(6);
            var dbCar = await _carRepository.GetById(carResponse.Id);
            dbCar.Should().BeEquivalentTo(car);
        }

        [Theory]
        [InlineData(3)]
        public async Task delete_CarWithValidData_RemovesCar(int carId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            await client.DeleteAsync($"/api/v1/cars/{carId}");

            // Assert
            _carRepository.Select().ToList().Should().HaveCount(5);
        }
    }
}
