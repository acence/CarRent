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
    public class IsDeleteCarCommandValidTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IsDeleteCarCommandValid _validator;

        public IsDeleteCarCommandValidTests()
        {
            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.DoesCarExistAsync(1)).ReturnsAsync(true);
            _carRepository.Setup(x => x.DoesCarExistAsync(2)).ReturnsAsync(false);

            _validator = new IsDeleteCarCommandValid(_carRepository.Object);
        }

        [Theory]
        [ClassData(typeof(DeleteCarValidData))]
        public async Task WhenValidatingDeleteCarCommand_WithValidData_ExpectNoError(DeleteCar.Command command)
        {
            var result = await _validator.ValidateAsync(command);

            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(DeleteCarInvalidData))]
        public async Task WhenValidatingDeleteCarCommand_WithInvalidData_ExpectError(DeleteCar.Command command, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
