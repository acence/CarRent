using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Database.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.UseCases.Rentals.Validators
{
    public class IsGetUpcomingRentalsQueryValid : AbstractValidator<GetUpcomingRentals.Query>
    {
        public IsGetUpcomingRentalsQueryValid(IUserRepository userRepository)
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (x, cancellation) => await userRepository.DoesUserExistAsync(x, cancellation))
                .WithMessage("Must request rentals for an existing user in the system")
                .WithErrorCode(ValidationErrorCodes.NotFound)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.From)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotEqual(DateTimeOffset.MinValue)
                .WithSeverity(Severity.Error);
        }
    }
}
