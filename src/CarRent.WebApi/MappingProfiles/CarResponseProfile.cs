using AutoMapper;
using CarRent.Domain;
using CarRent.WebApi.Models.Response;

namespace CarRent.WebApi.MappingProfiles
{
    public class CarResponseProfile : Profile
    {
        public CarResponseProfile()
        {
            CreateMap<Car, CarResponse>();
        }
    }
}
