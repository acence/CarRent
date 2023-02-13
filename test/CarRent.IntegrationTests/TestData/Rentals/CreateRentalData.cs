using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.WebApi.Models.Request.Rentals;

namespace CarRent.IntegrationTests.TestData.Rentals
{
    public class CreateRentalData : TheoryData<CreateRentalRequest>
    {
        public CreateRentalData()
        {
            Add(new CreateRentalRequest() { CarId = 5, UserId = 1, From = DateTimeOffset.Now.Date.AddHours(10), To = DateTimeOffset.Now.Date.AddHours(11) });
        }
    }
}
