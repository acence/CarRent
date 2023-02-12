using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.IntegrationTests.TestData.Cars
{
    public class CreateCarData : TheoryData<CreateNewCar.Command>
    {
        public CreateCarData()
        {
            Add(new CreateNewCar.Command { Make = "TestCar", Model = "TestModel", UniqueId = "C123" });
        }
    }
}
