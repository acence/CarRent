using CarRent.WebApi.Models.Request.Car;

namespace CarRent.IntegrationTests.TestData.Cars
{
    public class UpdateCarData : TheoryData<int, UpdateCarRequest>
    {
        public UpdateCarData()
        {
            Add(4, new UpdateCarRequest { Make = "TestCar2", Model = "TestModel2", UniqueId = "C1234" });
        }
    }
}
