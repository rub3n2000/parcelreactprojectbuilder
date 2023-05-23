using AutoMapper;
using DotnetReact.Models;
using DotnetReact.Models.Dto;

namespace DotnetReact.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForProfile>();
            CreateMap<UserForProfile, User>();
            CreateMap<UserForAuth, User>();
            CreateMap<User, UserForAuth>();
            CreateMap<UserForAuth, UserForProfile>();
            CreateMap<UserForProfile, UserForAuth>();
        }
    }
}