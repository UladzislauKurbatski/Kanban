using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.DataAccess.Entities;

namespace Kanban.BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();
        }
    }
}
