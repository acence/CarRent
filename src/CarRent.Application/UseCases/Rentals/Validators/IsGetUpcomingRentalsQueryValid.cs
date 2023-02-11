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
                .NotEmpty()
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UserId)
                .Must(x => userRepository.DoesUserExistAsync(x).GetAwaiter().GetResult())
                .WithMessage("Must request rentals for an existing user in the system")
                .WithSeverity(Severity.Error);
        }
    }
}
