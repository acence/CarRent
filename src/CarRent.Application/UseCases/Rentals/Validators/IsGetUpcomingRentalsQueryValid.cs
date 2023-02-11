using CarRent.Application.UseCases.Rentals.Handlers;
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
        public IsGetUpcomingRentalsQueryValid()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithSeverity(Severity.Error);
        }
    }
}
