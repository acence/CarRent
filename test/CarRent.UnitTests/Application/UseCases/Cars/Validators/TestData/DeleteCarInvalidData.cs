using CarRent.Application.UseCases.Cars.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class DeleteCarInvalidData : TheoryData<DeleteCar.Command, IList<(string property, string errorCode)>>
    {
        public DeleteCarInvalidData()
        {
            Add(new DeleteCar.Command() { Id = 0 }, new List<(string, string)> { ("Id", ValidationErrorCodes.NotEmpty) });
            Add(new DeleteCar.Command() { Id = 2 }, new List<(string, string)> { ("Id", ValidationErrorCodes.Predicate) });
        }
    }
}
