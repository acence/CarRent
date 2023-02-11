using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.IntegrationTests.ClassData.Cars
{
    public class UpdateCarData : TheoryData<UpdateCar.Command>
    {
        public UpdateCarData()
        {
            Add(new UpdateCar.Command { Id = 4, Make = "TestCar2", Model = "TestModel2", UniqueId = "C1234" });
        }
    }
}
