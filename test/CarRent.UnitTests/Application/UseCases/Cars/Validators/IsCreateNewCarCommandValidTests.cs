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
    public class IsCreateNewCarCommandValidTests
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly IsCreateNewCarCommandValid _validator;

        public IsCreateNewCarCommandValidTests()
        {
            _carRepository = new Mock<ICarRepository>();
            _carRepository.Setup(x => x.IsCarUniqueIdInUseAsync("C1")).ReturnsAsync(false);
            _carRepository.Setup(x => x.IsCarUniqueIdInUseAsync("C2")).ReturnsAsync(true);

            _validator = new IsCreateNewCarCommandValid(_carRepository.Object);
        }

        [Theory]
        [ClassData(typeof(CreateNewCarValidData))]
        public void WhenValidatingCreateNewCarCommand_WithValidData_ExpectNoError(CreateNewCar.Command command)
        {
            Action action = () => _validator.ValidateAndThrow(command);

            // Assert/Act
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CreateNewCarInvalidData))]
        public async Task WhenValidatingCreateNewCarCommand_WithInvalidData_ExpectError(CreateNewCar.Command command, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
