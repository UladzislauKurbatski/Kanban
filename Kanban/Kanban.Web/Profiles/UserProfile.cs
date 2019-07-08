using AutoMapper;
using Kanban.Api.Models.User;
using Kanban.BusinessLogic.DTOs;

namespace Kanban.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
        }
    }
}
