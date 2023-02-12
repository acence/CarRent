using CarRent.Application.UseCases.Cars.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class UpdateCarValidData : TheoryData<UpdateCar.Command>
    {
        public UpdateCarValidData()
        {
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "TestModel", UniqueId = "C12345" });
        }
    }
}
