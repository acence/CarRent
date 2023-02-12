using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.IntegrationTests.TestData.Rentals
{
    public class CreateRentalData : TheoryData<CreateRental.Command>
    {
        public CreateRentalData()
        {
            Add(new CreateRental.Command() { CarId = 5, UserId = 1, Date = DateOnly.FromDateTime(DateTimeOffset.Now.Date) });
        }
    }
}
