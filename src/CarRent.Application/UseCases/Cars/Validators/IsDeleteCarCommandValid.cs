using CarRent.Application.UseCases.Cars.Handlers;
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
        public IsDeleteCarCommandValid()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithSeverity(Severity.Error);
        }
    }
}
