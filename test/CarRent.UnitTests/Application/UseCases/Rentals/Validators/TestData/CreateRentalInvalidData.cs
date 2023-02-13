using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class CreateRentalInvalidData : TheoryData<CreateRental.Command, IList<(string property, string errorCode)>>
    {
        public CreateRentalInvalidData()
        {
            Add(new CreateRental.Command { UserId = 0, CarId = 1, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(1) }, new List<(string, string)> { ("UserId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 2, CarId = 1, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(1) }, new List<(string, string)> { ("UserId", ValidationErrorCodes.NotFound) });

            Add(new CreateRental.Command { UserId = 1, CarId = 0, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(1) }, new List<(string, string)> { ("CarId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 1, CarId = 2, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(1) }, new List<(string, string)> { ("CarId", ValidationErrorCodes.NotFound) });

            Add(new CreateRental.Command { UserId = 1, CarId = 1, From = DateTimeOffset.MinValue, To = DateTimeOffset.Now }, new List<(string, string)> { ("From", ValidationErrorCodes.NotEmpty), ("To", ValidationErrorCodes.TooLong) });
            Add(new CreateRental.Command { UserId = 1, CarId = 1, From = DateTimeOffset.Now.AddDays(3), To = DateTimeOffset.Now.AddDays(3).AddHours(1) }, new List<(string, string)> { ("From", ValidationErrorCodes.LessThan) });

            Add(new CreateRental.Command { UserId = 1, CarId = 1, From = DateTimeOffset.Now, To = DateTimeOffset.MinValue }, new List<(string, string)> { ("To", ValidationErrorCodes.NotEmpty) });
            Add(new CreateRental.Command { UserId = 1, CarId = 1, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(-1) }, new List<(string, string)> { ("To", ValidationErrorCodes.GreaterThan) });

            Add(new CreateRental.Command { UserId = 1, CarId = 1, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(3) }, new List<(string, string)> { ("To", ValidationErrorCodes.TooLong) });

            Add(new CreateRental.Command { UserId = 1, CarId = 3, From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddHours(1) }, new List<(string, string)> { ("From", ValidationErrorCodes.NotAvailable) });
        }
    }
}
