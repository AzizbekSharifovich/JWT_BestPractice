using AutoMapper;
using Entities.DTO.User;
using Entities.Models.User;

namespace JWT_WebAPI.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
    }
}
