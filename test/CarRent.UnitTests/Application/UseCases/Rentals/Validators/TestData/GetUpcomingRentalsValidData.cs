using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class GetUpcomingRentalsValidData : TheoryData<GetUpcomingRentals.Query>
    {
        public GetUpcomingRentalsValidData()
        {
            Add(new GetUpcomingRentals.Query { UserId = 1, From = DateTime.Now.Date });
        }
    }
}
