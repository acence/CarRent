using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Cars.Validators;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Application.UseCases.Rentals.Validators;
using CarRent.Database.Interfaces.Repositories;
using CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData;
using CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators
{
    [Collection("Rental")]
    public class IsGetAvailableCarsQueryValidTests
    {
        private readonly IsGetAvailableCarsQueryValid _validator;

        public IsGetAvailableCarsQueryValidTests()
        {
            _validator = new IsGetAvailableCarsQueryValid();
        }
        [Theory]
        [ClassData(typeof(GetAvailableCarsValidData))]
        public void WhenValidatingGetAvailableCarsQuery_WithValidData_ExpectNoError(GetAvailableCars.Query query)
        {
            Action action = () => _validator.ValidateAndThrow(query);

            // Assert/Act
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(GetAvailableCarsInvalidData))]
        public async Task WhenValidatingGetAvailableCarsQuery_WithInvalidData_ExpectError(GetAvailableCars.Query query, IList<(string property, string errorCode)> expectedErrors)
        {
            ValidationResult result = await _validator.ValidateAsync(query);

            // Assert
            IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
            validationErrors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
