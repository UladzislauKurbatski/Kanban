using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.DataAccess.Entities;

namespace Kanban.BusinessLogic.Profiles
{
    public class TaskTypeProfile : Profile
    {
        public TaskTypeProfile()
        {
            CreateMap<TaskTypeEntity, TaskTypeDto>();
            CreateMap<TaskTypeDto, TaskTypeEntity>();
        }
    }
}
