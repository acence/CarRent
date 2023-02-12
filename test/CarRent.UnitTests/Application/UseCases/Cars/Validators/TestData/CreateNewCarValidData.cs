using CarRent.Application.UseCases.Cars.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class CreateNewCarValidData : TheoryData<CreateNewCar.Command>
    {
        public CreateNewCarValidData()
        {
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "TestModel", UniqueId = "C12345" });
        }
    }
}
