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
    public class CreateCarData : TheoryData<CreateNewCarRequest>
    {
        public CreateCarData()
        {
            Add(new CreateNewCarRequest { Make = "TestCar", Model = "TestModel", UniqueId = "C123" });
        }
    }
}
