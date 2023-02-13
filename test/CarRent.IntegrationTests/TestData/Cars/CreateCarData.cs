using CarRent.WebApi.Models.Request.Car;

namespace CarRent.IntegrationTests.TestData.Cars
{
    public class CreateCarData : TheoryData<CreateNewCarRequest>
    {
        public CreateCarData()
        {
            Add(new CreateNewCarRequest { Make = "TestCar", Model = "TestModel", UniqueId = "C123" });
        }
    }
}
