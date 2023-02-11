using AutoMapper;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.MappingProfiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<CreateRental.Command, Rental>()
                .ForMember(dest => dest.RentDateTime, opt => opt.MapFrom(src => src.Date));
        }
    }
}
