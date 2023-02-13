using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;

namespace CarRent.UnitTests.Application.UseCases.Cars.Handlers
{
    [Collection("Car")]
    public class GetAllCarTests
    {
        private readonly Mock<ICarRepository> _carRepository;

        public GetAllCarTests()
        {
            _carRepository = new Mock<ICarRepository>();
            _carRepository
                .Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Car> { new Car { Id = 1 } });
        }


        [Fact]
        public async Task WhenCallingGetAllCars_WithEmptyQuery_ExpectNoErrors()
        {
            // Arrange
            var handler = new GetAllCars(_carRepository.Object);
            var query = new GetAllCars.Query();
            var response = await handler.Handle(query, CancellationToken.None);

            response.Should().NotBeNull();
            response.Count().Should().Be(1);
        }

        [Fact]
        public async Task WhenCallingGetAllCars_WithValidParameters_ExpectNoErrors()
        {
            // Arrange
            var handler = new GetAllCars(_carRepository.Object);
            var query = new GetAllCars.Query { Make = "TestMake", Model = "TestModel", UniqueId = "C123" };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(1);
        }

        [Fact]
        public async Task WhenCallingGetAllCars_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var query = new GetAllCars.Query();
            Func<Task> result = async () => await new GetAllCars(null!).Handle(query, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
