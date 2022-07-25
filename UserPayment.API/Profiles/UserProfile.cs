using AutoMapper;
using UserPayment.API.Entities;
using UserPayment.API.Models;

namespace UserPayment.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserWithoutPaymentsDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForUpdateDto>();
        }

    }
}
