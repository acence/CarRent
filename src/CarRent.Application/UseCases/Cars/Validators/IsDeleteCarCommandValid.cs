using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.UseCases.Cars.Validators
{
    public class IsDeleteCarCommandValid : AbstractValidator<DeleteCar.Command>
    {
        public IsDeleteCarCommandValid(ICarRepository carRepository)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync( async (x, cancellation) => await carRepository.DoesCarExistAsync(x))
                .WithMessage("Must update a car that exists in the system")
                .WithSeverity(Severity.Error);
        }
    }
}
