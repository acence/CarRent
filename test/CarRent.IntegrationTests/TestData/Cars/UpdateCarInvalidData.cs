using CarRent.WebApi.Models.Request.Car;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CarRent.IntegrationTests.TestData.Cars
{
    public class UpdateCarInvalidData : TheoryData<int, UpdateCarRequest, int, string>
    {
        public UpdateCarInvalidData()
        {
            Add(44, new UpdateCarRequest { Make = "TestCar2", Model = "TestModel2", UniqueId = "C1234" }, StatusCodes.Status404NotFound, "Car not found");
            Add(4, new UpdateCarRequest { Make = "TestCar2", Model = "TestModel2", UniqueId = "" }, StatusCodes.Status400BadRequest, "UniqueId is required");
        }
    }
}
