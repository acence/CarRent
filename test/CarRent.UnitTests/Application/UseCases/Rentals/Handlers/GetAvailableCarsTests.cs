using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FluentAssertions;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Handlers
{
    [Collection("Rental")]
    public class GetAvailableCarsTests
    {
        private readonly Mock<ICarRepository> _carRepository;

        public GetAvailableCarsTests()
        {
            _carRepository = new Mock<ICarRepository>();
            _carRepository
                .Setup(x => x.GetAvailableCarsAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Car> { new Car { Id = 1 } });
        }
        [Fact]
        public async Task WhenCallingGetAvailableCars_WithDate_ExpectNoErrors()
        {
            // Arrange
            var handler = new GetAvailableCars(_carRepository.Object);
            var query = new GetAvailableCars.Query();
            var response = await handler.Handle(query, CancellationToken.None);

            response.Should().NotBeNull();
            response.Count().Should().Be(1);
        }

        [Fact]
        public async Task WhenCallingGetAvailableCars_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var query = new GetAvailableCars.Query();
            Func<Task> result = async () => await new GetAvailableCars(null!).Handle(query, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
