using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class CreateRentalInvalidData : TheoryData<CreateRental.Command, IList<(string property, string errorCode)>>
    {
        public CreateRentalInvalidData()
        {
            Add(new CreateRental.Command { UserId = 0, CarId = 1, Date = DateOnly.FromDateTime(DateTime.Now) }, new List<(string, string)> { ("UserId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 2, CarId = 1, Date = DateOnly.FromDateTime(DateTime.Now) }, new List<(string, string)> { ("UserId", ValidationErrorCodes.Predicate) });

            Add(new CreateRental.Command { UserId = 1, CarId = 0, Date = DateOnly.FromDateTime(DateTime.Now) }, new List<(string, string)> { ("CarId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 1, CarId = 2, Date = DateOnly.FromDateTime(DateTime.Now) }, new List<(string, string)> { ("CarId", ValidationErrorCodes.Predicate) });

            Add(new CreateRental.Command { UserId = 1, CarId = 1, Date = DateOnly.MinValue }, new List<(string, string)> { ("Date", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 1, CarId = 3, Date = DateOnly.FromDateTime(DateTime.Now) }, new List<(string, string)> { ("Date", ValidationErrorCodes.Predicate) });
        }
    }
}
