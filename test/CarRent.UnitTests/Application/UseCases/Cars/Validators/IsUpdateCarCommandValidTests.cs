using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Cars.Validators;
using CarRent.Database.Interfaces.Repositories;
using CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators
{
    [Collection("Car")]
    public class IsUpdateCarCommandValidTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IsUpdateCarCommandValid _validator;

        public IsUpdateCarCommandValidTests()
        {
            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.DoesCarExistAsync(1)).ReturnsAsync(true);
            _carRepository.Setup(x => x.DoesCarExistAsync(2)).ReturnsAsync(false);
            _carRepository.Setup(x => x.IsCarUniqueIdInUseAsync("C1")).ReturnsAsync(false);
            _carRepository.Setup(x => x.IsCarUniqueIdInUseAsync("C2")).ReturnsAsync(true);

            _validator = new IsUpdateCarCommandValid(_carRepository.Object);
        }

        [Theory]
        [ClassData(typeof(UpdateCarValidData))]
        public void WhenValidatingUpdateCarCommand_WithValidData_ExpectNoError(UpdateCar.Command command)
        {
            Action action = () => _validator.ValidateAndThrow(command);

            // Assert/Act
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(UpdateCarInvalidData))]
        public async Task WhenValidatingUpdateCarCommand_WithInvalidData_ExpectError(UpdateCar.Command command, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
