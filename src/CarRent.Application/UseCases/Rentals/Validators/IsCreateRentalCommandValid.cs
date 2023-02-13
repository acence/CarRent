using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.UseCases.Rentals.Validators
{
    public class IsCreateRentalCommandValid : AbstractValidator<CreateRental.Command>
    {
        public IsCreateRentalCommandValid(IUserRepository userRepository, ICarRepository carRepository, IRentalRepository rentalRepository)
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => userRepository.DoesUserExistAsync(x).GetAwaiter().GetResult())
                .WithMessage("Must request rentals for an existing user in the system")
                .WithSeverity(Severity.Error);

            RuleFor(x => x.CarId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => carRepository.DoesCarExistAsync(x).GetAwaiter().GetResult())
                .WithMessage("Must request rentals for an existing car in the system")
                .WithSeverity(Severity.Error);

            RuleFor(x => x.From)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotEqual(DateTimeOffset.MinValue)
                .LessThan(DateTime.Now.Date.AddDays(1))
                .WithSeverity(Severity.Error);

            RuleFor(x => x.To )
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotEqual(DateTimeOffset.MinValue)
                .GreaterThan(x => x.From)
                .Must((command, _) => (command.To - command.From).TotalHours <= 2)
                .WithMessage("Rental duration cannot exceed two hours")
                .WithSeverity(Severity.Error);

            RuleFor(x => x.From)
                .Cascade(CascadeMode.Stop)
                .Must((command, _) => !rentalRepository.DoesRentalExistForCarAsync(command.From, command.To, command.CarId).GetAwaiter().GetResult())
                .WithName("(From, To)")
                .WithMessage("Must request rentals for an available car in the system")
                .WithSeverity(Severity.Error);


        }
    }
}
