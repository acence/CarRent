using AutoMapper;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Application.MappingProfiles;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FluentAssertions;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Cars.Handlers
{
    [Collection("Car")]
    public class CreateNewCarTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IMapper _mapper;

        private readonly CreateNewCar.Command _successData;
        private readonly CreateNewCar.Command _failData;

        public CreateNewCarTests()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarProfile());
            });

            _successData = new CreateNewCar.Command { UniqueId = "C1" };
            _failData = new CreateNewCar.Command { UniqueId = "C2" };

            _mapper = new Mapper(mapperConfig);

            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.Insert(It.Is<Car>(x => x.UniqueId == _successData.UniqueId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _carRepository.Setup(x => x.Insert(It.Is<Car>(x => x.UniqueId == _failData.UniqueId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
        }

        [Fact]
        public async Task WhenCallingCreateNewCar_WithValidCommand_ExpectNoErrors()
        {
            var handler = new CreateNewCar(_carRepository.Object, _mapper);
            var result = await handler.Handle(_successData, CancellationToken.None);

            result.Should().NotBeNull();
            result.UniqueId.Should().Be(_successData.UniqueId);
        }

        [Fact]
        public async Task WhenCallingCreateNewCar_WithCommandWhichFails_ExpectErrors()
        {

            Func<Task> result = async () => await new CreateNewCar(_carRepository.Object, _mapper).Handle(_failData, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<CarNotCreatedException>();
        }

        [Fact]
        public async Task WhenCallingCreateNewCar_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var command = new CreateNewCar.Command();
            Func<Task> result = async () => await new CreateNewCar(null!, _mapper).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCallingCreateNewCar_WithoutMapper_ExpectErrors()
        {
            // Arrange;
            var command = new CreateNewCar.Command();
            Func<Task> result = async () => await new CreateNewCar(_carRepository.Object, null!).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
