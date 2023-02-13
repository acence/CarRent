using AutoMapper;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.WebApi.Models.Request.Car;

namespace CarRent.WebApi.MappingProfiles
{
    public class CarRequestProfile : Profile
    {
        public CarRequestProfile()
        {
            CreateMap<CreateNewCarRequest, CreateNewCar.Command>();

            CreateMap<UpdateCarRequest, UpdateCar.Command>();
        }
    }
}
