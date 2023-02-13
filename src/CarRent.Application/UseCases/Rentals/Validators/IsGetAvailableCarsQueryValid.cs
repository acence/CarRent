using CarRent.Application.UseCases.Rentals.Handlers;
using FluentValidation;

namespace CarRent.Application.UseCases.Rentals.Validators
{
    public class IsGetAvailableCarsQueryValid : AbstractValidator<GetAvailableCars.Query>
    {
        public IsGetAvailableCarsQueryValid()
        {
            RuleFor(x => x.From)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotEqual(DateTimeOffset.MinValue)
                .WithSeverity(Severity.Error);
        }
    }
}
