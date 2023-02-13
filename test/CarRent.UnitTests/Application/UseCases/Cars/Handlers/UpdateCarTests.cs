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
    public class UpdateCarTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IMapper _mapper;

        private readonly UpdateCar.Command _successData;
        private readonly UpdateCar.Command _failData;

        public UpdateCarTests()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarProfile());
            });

            _successData = new UpdateCar.Command { Id = 1 };
            _failData = new UpdateCar.Command { Id = 2 };

            _mapper = new Mapper(mapperConfig);

            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.Update(It.Is<Car>(x => x.Id == _successData.Id)))
                .ReturnsAsync(1);
            _carRepository.Setup(x => x.Update(It.Is<Car>(x => x.Id == _failData.Id)))
                .ReturnsAsync(0);
        }

        [Fact]
        public async Task WhenCallingUpdateCar_WithValidCommand_ExpectNoErrors()
        {
            var handler = new UpdateCar(_carRepository.Object, _mapper);
            var result = await handler.Handle(_successData, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(_successData.Id);
        }

        [Fact]
        public async Task WhenCallingUpdateCar_WithCommandWhichFails_ExpectErrors()
        {

            Func<Task> result = async () => await new UpdateCar(_carRepository.Object, _mapper).Handle(_failData, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<CarNotUpdatedException>();
        }

        [Fact]
        public async Task WhenCallingUpdateCar_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var command = new UpdateCar.Command();
            Func<Task> result = async () => await new UpdateCar(null!, _mapper).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCallingUpdateCar_WithoutMapper_ExpectErrors()
        {
            // Arrange;
            var command = new UpdateCar.Command();
            Func<Task> result = async () => await new UpdateCar(_carRepository.Object, null!).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
