using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Cars.Handlers;
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
    }
}
