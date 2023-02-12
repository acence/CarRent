using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class GetAvailableCarsValidData : TheoryData<GetAvailableCars.Query>
    {
        public GetAvailableCarsValidData()
        {
            Add(new GetAvailableCars.Query { Date = DateOnly.FromDateTime(DateTime.Now) });
        }
    }
}
