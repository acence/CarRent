using AutoMapper;
using CarRent.Application.Exceptions.CarExceptions;
using CarRent.Application.Exceptions.RentalExceptions;
using CarRent.Application.MappingProfiles;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using CarRent.Domain;
using FluentAssertions;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Handlers
{
    [Collection("Rental")]
    public class CreateRentalTests
    {
        private readonly Mock<IRentalRepository> _rentalRepository;
        private readonly IMapper _mapper;

        private readonly CreateRental.Command _successData;
        private readonly CreateRental.Command _failData;

        public CreateRentalTests()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarProfile());
                cfg.AddProfile(new RentalProfile());
            });

            _successData = new CreateRental.Command { CarId = 1 };
            _failData = new CreateRental.Command { CarId = 2 };

            _mapper = new Mapper(mapperConfig);

            _rentalRepository = new Mock<IRentalRepository>();
            _rentalRepository.Setup(x => x.GetByIdWithParentsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(new Rental { CarId = 1 } );
            _rentalRepository.Setup(x => x.Insert(It.Is<Rental>(x => x.CarId == _successData.CarId), It.IsAny<CancellationToken>()))
                .Callback<Rental, CancellationToken>((x, cancellation) => x.Id = 1)
                .ReturnsAsync(1);
            _rentalRepository.Setup(x => x.Insert(It.Is<Rental>(x => x.CarId == _failData.CarId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
        }
        [Fact]
        public async Task WhenCallingCreateRental_WithValidCommand_ExpectNoErrors()
        {
            var handler = new CreateRental(_rentalRepository.Object, _mapper);
            var result = await handler.Handle(_successData, CancellationToken.None);

            result.Should().NotBeNull();
            result.CarId.Should().Be(_successData.CarId);
        }
        [Fact]
        public async Task WhenCallingCreateRental_WithCommandWhichFails_ExpectErrors()
        {

            Func<Task> result = async () => await new CreateRental(_rentalRepository.Object, _mapper).Handle(_failData, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<RentalNotCreatedException>();
        }

        [Fact]
        public async Task WhenCallingCreateRental_WithoutRepository_ExpectErrors()
        {
            // Arrange;
            var command = new CreateRental.Command();
            Func<Task> result = async () => await new CreateRental(null!, _mapper).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCallingCreateRental_WithoutMapper_ExpectErrors()
        {
            // Arrange;
            var command = new CreateRental.Command();
            Func<Task> result = async () => await new CreateRental(_rentalRepository.Object, null!).Handle(command, CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
