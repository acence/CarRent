using CarRent.Application.UseCases.Cars.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
