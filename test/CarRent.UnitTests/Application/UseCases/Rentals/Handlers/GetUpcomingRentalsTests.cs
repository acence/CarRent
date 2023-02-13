using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FluentAssertions;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Handlers
{
    [Collection("Rental")]
    public class GetUpcomingRentalsTests
    {
        private readonly Mock<IRentalRepository> _rentalRepository;

        public GetUpcomingRentalsTests()
        {
            _rentalRepository = new Mock<IRentalRepository>();
            _rentalRepository
                .Setup(x => x.GetRentalsByUserIdAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Rental> { new Rental { Id = 1, CarId = 1, UserId = 1 } });
        }
        [Fact]
        public async Task WhenCallingGetUpcomingRentals_WithDate_ExpectNoErrors()
        {
            // Arrange
            var handler = new GetUpcomingRentals(_rentalRepository.Object);
            var query = new GetUpcomingRentals.Query();
            var response = await handler.Handle(query, CancellationToken.None);

            response.Should().NotBeNull();
            response.Count().Should().Be(1);
        }

        [Fact]
        public async Task WhenCallingGetUpcomingRentals_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var query = new GetUpcomingRentals.Query();
            Func<Task> result = async () => await new GetUpcomingRentals(null!).Handle(query, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
