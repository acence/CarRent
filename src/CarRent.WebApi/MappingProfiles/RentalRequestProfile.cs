using AutoMapper;
using CarRent.Application.UseCases.Rentals.Handlers;
using CarRent.WebApi.Models.Request.Rentals;

namespace CarRent.WebApi.MappingProfiles
{
    public class RentalRequestProfile : Profile
    {
        public RentalRequestProfile()
        {
            CreateMap<CreateRentalRequest, CreateRental.Command>();
        }
    }
}
