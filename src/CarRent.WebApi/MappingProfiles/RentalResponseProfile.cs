using AutoMapper;
using CarRent.Domain;
using CarRent.WebApi.ResponseModels;

namespace CarRent.WebApi.MappingProfiles
{
    public class RentalResponseProfile : Profile
    {
        public RentalResponseProfile()
        {
            CreateMap<Rental, RentalResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(x => x.RentDate));
        }
    }
}
