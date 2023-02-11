using AutoMapper;
using CarRent.Application.UseCases.Cars.Handlers;
using CarRent.Domain;

namespace CarRent.Application.MappingProfiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CreateNewCar.Command, Car>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Rentals, opt => opt.Ignore());

            CreateMap<UpdateCar.Command, Car>()
                .ForMember(x => x.Rentals, opt => opt.Ignore());

            CreateMap<DeleteCar.Command, Car>()
                .ForMember(x => x.Make, opt => opt.Ignore())
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.UniqueId, opt => opt.Ignore())
                .ForMember(x => x.Rentals, opt => opt.Ignore());
        }
    }
}
