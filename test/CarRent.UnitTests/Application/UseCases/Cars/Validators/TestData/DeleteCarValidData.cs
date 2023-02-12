using CarRent.Application.UseCases.Cars.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class DeleteCarValidData : TheoryData<DeleteCar.Command>
    {
        public DeleteCarValidData()
        {
            Add(new DeleteCar.Command() {Id = 1 });
        }
    }
}
