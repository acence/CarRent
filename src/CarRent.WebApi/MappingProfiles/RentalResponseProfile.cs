using AutoMapper;
using CarRent.Domain;
using CarRent.WebApi.Models.Response;

namespace CarRent.WebApi.MappingProfiles
{
    public class RentalResponseProfile : Profile
    {
        public RentalResponseProfile()
        {
            CreateMap<Rental, RentalResponse>();
        }
    }
}
