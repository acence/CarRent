using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Application.UseCases.Cars.Validators;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Application.UseCases.Rentals.Validators;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarRent.Application.Configuration
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(ServicesConfig))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            var typesToRegister = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace)
                    && type.BaseType != null
                    && type.BaseType.IsGenericType 
                    && type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));

            foreach(var type in typesToRegister)
            {
                var validationInterface = type.GetInterfaces()
                    .Where(x => x.IsGenericType 
                        && x.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .First();

                services.AddTransient(validationInterface, type);
            }

            return services;
        }
    }
}
