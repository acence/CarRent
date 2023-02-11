using AutoMapper;
using CarRent.Domain;
using CarRent.WebApi.ResponseModels;

namespace CarRent.WebApi.MappingProfiles
{
    public class CarResponseProfile : Profile
    {
        public CarResponseProfile()
        {
            CreateMap<CarResponse, Car>();
        }
    }
}
