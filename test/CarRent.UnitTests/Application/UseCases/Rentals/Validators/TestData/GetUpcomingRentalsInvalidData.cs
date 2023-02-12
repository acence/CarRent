using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class GetUpcomingRentalsInvalidData : TheoryData<GetUpcomingRentals.Query, IList<(string property, string errorCode)>>
    {
        public GetUpcomingRentalsInvalidData()
        {
            Add(new GetUpcomingRentals.Query { UserId = 0 }, new List<(string, string)> { ("UserId", ValidationErrorCodes.NotEmpty) });
            Add(new GetUpcomingRentals.Query { UserId = 2 }, new List<(string, string)> { ("UserId", ValidationErrorCodes.Predicate) });
        }
    }
}
