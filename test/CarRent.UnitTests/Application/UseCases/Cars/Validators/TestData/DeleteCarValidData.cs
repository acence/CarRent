using CarRent.Application.UseCases.Cars.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class DeleteCarValidData : TheoryData<DeleteCar.Command>
    {
        public DeleteCarValidData()
        {
            Add(new DeleteCar.Command() {Id = 1 });
        }
    }
}
