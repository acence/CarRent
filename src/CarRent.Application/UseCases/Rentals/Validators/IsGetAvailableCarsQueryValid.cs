using CarRent.Application.UseCases.Rentals.Handlers;
using FluentValidation;

namespace CarRent.Application.UseCases.Rentals.Validators
{
    public class IsGetAvailableCarsQueryValid : AbstractValidator<GetAvailableCars.Query>
    {
        public IsGetAvailableCarsQueryValid()
        {
            RuleFor(x => x.Date)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotEqual(DateOnly.MinValue)
                .WithSeverity(Severity.Error);
        }
    }
}
