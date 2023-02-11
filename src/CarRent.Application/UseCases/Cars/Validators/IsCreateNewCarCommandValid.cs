using CarRent.Application.UseCases.Cars.Handlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.UseCases.Cars.Validators
{
    public class IsCreateNewCarCommandValid : AbstractValidator<CreateNewCar.Command>
    {
        public IsCreateNewCarCommandValid()
        {
            RuleFor(x => x.Make)
                .NotEmpty()
                .MaximumLength(50)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(120)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UniqueId)
                .NotEmpty()
                .MaximumLength(13)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UniqueId)
                .Must(x => x.StartsWith("C"));
        }
    }
}
