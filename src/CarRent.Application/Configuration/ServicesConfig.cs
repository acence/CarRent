using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Cars.Validators;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Application.UseCases.Rentals.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRent.Application.Configuration
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(GetAllCars.Query))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddTransient<IValidator<CreateNewCar.Command>, IsCreateNewCarCommandValid>()
                .AddTransient<IValidator<UpdateCar.Command>, IsUpdateCarCommandValid>()
                .AddTransient<IValidator<DeleteCar.Command>, IsDeleteCarCommandValid>()
                .AddTransient<IValidator<CreateRental.Command>, IsCreateRentalCommandValid>()
                .AddTransient<IValidator<GetAvailableCars.Query>, IsGetAvailableCarsQueryValid>()
                .AddTransient<IValidator<GetUpcomingRentals.Query>, IsGetUpcomingRentalsQueryValid>();
        }
    }
}
