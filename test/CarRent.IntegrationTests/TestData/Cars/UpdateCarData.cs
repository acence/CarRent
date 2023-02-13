using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;
using CarRent.WebApi.Models.Request.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
