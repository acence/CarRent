using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Application.UseCases.Rentals.Validators;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators
{
    public class IsCreateRentalCommandValidTests
    {
        private readonly IsCreateRentalCommandValid _validator;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICarRepository> _carRepository;
        private readonly Mock<IRentalRepository> _rentalRepository;

        public IsCreateRentalCommandValidTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.DoesUserExistAsync(1)).ReturnsAsync(true);
            _userRepository.Setup(x => x.DoesUserExistAsync(2)).ReturnsAsync(false);
            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.DoesCarExistAsync(1)).ReturnsAsync(true);
            _carRepository.Setup(x => x.DoesCarExistAsync(2)).ReturnsAsync(false);
            _carRepository.Setup(x => x.DoesCarExistAsync(3)).ReturnsAsync(true);
            _rentalRepository = new Mock<IRentalRepository>();
            _rentalRepository.Setup(x => x.DoesRentalExistForCarAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), 1)).ReturnsAsync(false);
            _rentalRepository.Setup(x => x.DoesRentalExistForCarAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), 3)).ReturnsAsync(true);
            _validator = new IsCreateRentalCommandValid(_userRepository.Object, _carRepository.Object, _rentalRepository.Object);
        }
        [Theory]
        [ClassData(typeof(CreateRentalValidData))]
        public void WhenValidatingGetAvailableCarsQuery_WithValidData_ExpectNoError(CreateRental.Command command)
        {
            Action action = () => _validator.ValidateAndThrow(command);

            // Assert/Act
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CreateRentalInvalidData))]
        public async Task WhenValidatingGetAvailableCarsQuery_WithInvalidData_ExpectError(CreateRental.Command command, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
