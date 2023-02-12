using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class CreateRentalValidData : TheoryData<CreateRental.Command>
    {
        public CreateRentalValidData()
        {
            Add(new CreateRental.Command { UserId = 1, CarId = 1, Date = DateOnly.FromDateTime(DateTime.Now) });
        }
    }
}
