using AutoMapper;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Application.MappingProfiles;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Domain;
using FluentAssertions;
using MediatR;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Cars.Handlers
{
    [Collection("Car")]
    public class DeleteCarTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IMapper _mapper;

        private readonly DeleteCar.Command _successData;
        private readonly DeleteCar.Command _failData;

        public DeleteCarTests()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarProfile());
            });

            _successData = new DeleteCar.Command { Id = 1 };
            _failData = new DeleteCar.Command { Id = 2 };

            _mapper = new Mapper(mapperConfig);

            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.Delete(It.Is<Car>(x => x.Id == _successData.Id)))
                .ReturnsAsync(1);
            _carRepository.Setup(x => x.Delete(It.Is<Car>(x => x.Id == _failData.Id)))
                .ReturnsAsync(0);
        }

        [Fact]
        public async Task WhenCallingDeleteCar_WithValidCommand_ExpectNoErrors()
        {
            var handler = new DeleteCar(_carRepository.Object, _mapper);
            var result = await handler.Handle(_successData, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task WhenCallingDeleteCar_WithCommandWhichFails_ExpectErrors()
        {

            Func<Task> result = async () => await new DeleteCar(_carRepository.Object, _mapper).Handle(_failData, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<CarNotDeletedException>();
        }

        [Fact]
        public async Task WhenCallingDeleteCar_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var command = new DeleteCar.Command();
            Func<Task> result = async () => await new DeleteCar(null!, _mapper).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCallingDeleteCar_WithoutMapper_ExpectErrors()
        {
            // Arrange;
            var command = new DeleteCar.Command();
            Func<Task> result = async () => await new DeleteCar(_carRepository.Object, null!).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
