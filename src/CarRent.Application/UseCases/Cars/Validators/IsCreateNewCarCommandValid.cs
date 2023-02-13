using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
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
        public IsCreateNewCarCommandValid(ICarRepository carRepository)
        {
            RuleFor(x => x.Make)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(50)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(120)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UniqueId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(13)
                .Must(x => x.StartsWith("C"))
                .WithMessage("Unique Id must start with C")
                .MustAsync(async (x, cancellation) => !await carRepository.IsCarUniqueIdInUseAsync(x))
                .WithMessage("Unique Id must already esists in system")
                .WithSeverity(Severity.Error);
        }
    }
}
