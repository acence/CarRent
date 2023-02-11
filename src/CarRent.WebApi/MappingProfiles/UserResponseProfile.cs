using AutoMapper;
using CarRent.Domain;
using CarRent.WebApi.ResponseModels;

namespace CarRent.WebApi.MappingProfiles
{
    public class UserResponseProfile : Profile
    {
        public UserResponseProfile() 
        {
            CreateMap<User, UserResponse>();
        }
    }
}
