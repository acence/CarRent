using CarRent.Application.UseCases.Rentals.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Rentals.Validators.TestData
{
    public class GetAvailableCarsInvalidData : TheoryData<GetAvailableCars.Query, IList<(string property, string errorCode)>>
    {
        public GetAvailableCarsInvalidData()
        {
            Add(new GetAvailableCars.Query { From = DateTimeOffset.MinValue }, new List<(string, string)> { ("From", ValidationErrorCodes.NotEmpty) });
        }
    }
}
