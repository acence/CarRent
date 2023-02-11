﻿using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.UseCases.Cars.Validators
{
    public class IsUpdateCarCommandValid : AbstractValidator<UpdateCar.Command>
    {
        public IsUpdateCarCommandValid(ICarRepository carRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithSeverity(Severity.Error);

            RuleFor(x => x.Id)
                .Must(x => carRepository.DoesCarExistAsync(x).GetAwaiter().GetResult())
                .WithMessage("Must update a car that exists in the system")
                .WithSeverity(Severity.Error);

            RuleFor(x => x.Make)
                .NotEmpty()
                .MaximumLength(50)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(120)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UniqueId)
                .NotEmpty()
                .MaximumLength(13)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.UniqueId)
                .Must(x => x.StartsWith("C"))
                .WithMessage("Unique Id must start with C")
                .WithSeverity(Severity.Error);
        }
    }
}
