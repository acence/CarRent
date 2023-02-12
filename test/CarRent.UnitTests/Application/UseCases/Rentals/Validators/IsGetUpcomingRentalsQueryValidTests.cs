using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Application.UseCases.Rentals.Validators;
using CarRent.Database.Interfaces.Repositories;
using CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators
{
    public class IsGetUpcomingRentalsQueryValidTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly IsGetUpcomingRentalsQueryValid _validator;

        public IsGetUpcomingRentalsQueryValidTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.DoesUserExistAsync(1)).ReturnsAsync(true);
            _userRepository.Setup(x => x.DoesUserExistAsync(2)).ReturnsAsync(false);

            _validator = new IsGetUpcomingRentalsQueryValid(_userRepository.Object);
        }
        [Theory]
        [ClassData(typeof(GetUpcomingRentalsValidData))]
        public void WhenValidatingGetAvailableCarsQuery_WithValidData_ExpectNoError(GetUpcomingRentals.Query query)
        {
            Action action = () => _validator.ValidateAndThrow(query);

            // Assert/Act
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(GetUpcomingRentalsInvalidData))]
        public async Task WhenValidatingGetAvailableCarsQuery_WithInvalidData_ExpectError(GetUpcomingRentals.Query query, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(query);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
